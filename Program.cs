using Microsoft.EntityFrameworkCore;
using minimal.Dominios.DTOS;
using minimal.Dominios.Interfaces;
using minimal.Infraestrutura.DB;
using minimal.Dominios.Servicos;
using Microsoft.AspNetCore.Mvc;
using minimal.Dominios.Entidades;
using minimal.Dominios.ModelViews;
using minimal.Dominios.Enus;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authorization;
#region builder
var builder = WebApplication.CreateBuilder(args);

var key = builder.Configuration.GetSection("Jwt").ToString();
if (String.IsNullOrEmpty(key)) key = "123456";
builder.Services.AddDbContext<DbContexto>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("mysql"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("mysql")));
});
builder.Services.AddScoped<iAdimistradorServico, AdiminstradorService>();
builder.Services.AddScoped<iVeiculoService, VeiculoService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT is needed to access the endpoints "
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
builder.Services.AddAuthentication(option =>
{
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});


builder.Services.AddAuthorization();
var app = builder.Build();
#endregion
#region Home
app.MapGet("/", () => Results.Json(new Home())).AllowAnonymous().WithTags("Home");
#endregion
#region Adimistradores
String GerarTokenJWT(Adiminstrador adm)
{
    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
    var claims = new List<Claim>(){
        new Claim("Email",adm.Email),
        new Claim("Perfil",adm.Perfil),
        new Claim(ClaimTypes.Role,adm.Perfil)
    };
    var token = new JwtSecurityToken(
        claims: claims,
        expires: DateTime.Now.AddDays(1),
        signingCredentials: credentials
    );
    return new JwtSecurityTokenHandler().WriteToken(token);
}
app.MapPost("/adiministradores/login", ([FromBody] LoginDTO loginDTO, iAdimistradorServico servico) =>
{
    ValidaLoginDTO valida = new ValidaLoginDTO();
    var validaRes = valida.valida(loginDTO);
    if (validaRes.Mensagem.Count > 0)
    {
        return Results.BadRequest(validaRes);
    }
    var adm = servico.Login(loginDTO);
    if (adm != null)
    {
        String tkn = GerarTokenJWT(adm);
        return Results.Ok(new AdmLogado { Email = adm.Email, Token = tkn, Perfil = adm.Perfil });
    }
    else
    {
        return Results.Unauthorized();
    }
}).AllowAnonymous().WithTags("Adiministradores");
app.MapPost("/administradores", ([FromBody] AdiministradorDTO adiminstrador, iAdimistradorServico servico) =>
{
    ValidaAdministrator valida = new ValidaAdministrator();
    var validaRes = valida.valida(adiminstrador);
    if (validaRes.Mensagem.Count > 0)
    {
        return Results.BadRequest(validaRes);
    }

    var adm = new Adiminstrador
    {
        Email = adiminstrador.Email,
        Senha = adiminstrador.Senha,
        Perfil = adiminstrador.Perfil.ToString(),
    };
    servico.Criar(adm);
    return Results.Ok();
}).RequireAuthorization().RequireAuthorization(new AuthorizeAttribute { Roles = "Adm" }).WithTags("Adiministradores");
app.MapGet("/administradores", (iAdimistradorServico servico) =>
{
    var adms = new List<AdministradorModelView>();
    var users = servico.Todos();
    foreach (var adm in users)
    {
        adms.Add(new AdministradorModelView
        {
            Id = adm.Id,
            Email = adm.Email,
            Perfil = (PerfilEnum)Enum.Parse(typeof(PerfilEnum), adm.Perfil)
        });
    }
    return Results.Ok(adms);
}).RequireAuthorization().RequireAuthorization(new AuthorizeAttribute { Roles = "Adm" }).WithTags("Adiministradores");
#endregion

#region Veiculos
app.MapPost("veiculos", ([FromBody] VeiculoDTO veiculoDTO, iVeiculoService service) =>
{
    ValidaVeiculoDTO validador = new ValidaVeiculoDTO();
    var validaResult = validador.validaDTO(veiculoDTO);
    if (validaResult.Mensagem.Count > 0)
    {
        return Results.BadRequest(validaResult);
    }
    var veiculo = new Veiculo
    {
        Nome = veiculoDTO.Nome,
        Ano = veiculoDTO.Ano,
        Marca = veiculoDTO.Marca
    };
    service.Incluir(veiculo);
    return Results.Created($"veiculo/{veiculo.Id}", veiculo);
}).RequireAuthorization().RequireAuthorization(new AuthorizeAttribute { Roles = "Adm,Editor" }).WithTags("Veiculos");
app.MapGet("veiculos", ([FromQuery] int pagina, iVeiculoService service) =>
{
    var veiculos = service.Todos(pagina);
    return Results.Ok(veiculos);
}).RequireAuthorization().RequireAuthorization(new AuthorizeAttribute { Roles = "Adm,Editor" }).WithTags("Veiculos");
app.MapGet("/veiculos/{id}", ([FromRoute] int id, iVeiculoService service) =>
{
    var veiculo = service.BuscaPorId(id);

    if (veiculo == null) return Results.NotFound();
    return Results.Ok(veiculo);
}).RequireAuthorization().WithTags("Veiculos");
app.MapPut("/veiculos/{id}", ([FromRoute] int id, VeiculoDTO veiculoDTO, iVeiculoService service) =>
{
    ValidaVeiculoDTO validador = new ValidaVeiculoDTO();
    var validaResult = validador.validaDTO(veiculoDTO);
    if (validaResult.Mensagem.Count > 0)
    {
        return Results.BadRequest(validaResult);
    }
    var veiculo = service.BuscaPorId(id);
    if (veiculo == null) return Results.NotFound();
    veiculo.Nome = veiculoDTO.Nome;
    veiculo.Marca = veiculoDTO.Marca;
    veiculo.Ano = veiculoDTO.Ano;
    service.Atualizar(veiculo);
    return Results.Ok(veiculo);
}).RequireAuthorization().RequireAuthorization(new AuthorizeAttribute { Roles = "Adm" }).WithTags("Veiculos");
app.MapDelete("/veiculos/{id}", ([FromRoute] int id, iVeiculoService service) =>
{
    var veiculo = service.BuscaPorId(id);
    if (veiculo == null) return Results.NotFound();
    service.Apagar(veiculo);
    return Results.Ok();
}).RequireAuthorization().RequireAuthorization(new AuthorizeAttribute { Roles = "Adm" }).WithTags("Veiculos");
#endregion

#region Swagger
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseAuthorization();
#endregion
app.Run();