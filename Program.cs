using Microsoft.EntityFrameworkCore;
using minimal.Dominios.DTOS;
using minimal.Dominios.Interfaces;
using minimal.Infraestrutura.DB;
using minimal.Dominios.Servicos;
using Microsoft.AspNetCore.Mvc;
using minimal.Dominios.Entidades;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DbContexto>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("mysql"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("mysql")));
});
builder.Services.AddScoped<iAdimistradorServico, AdiminstradorService>();
builder.Services.AddScoped<iVeiculoService, VeiculoService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

#region Home
app.MapGet("/", () => Results.Json(new Home())).WithTags("Home");
#endregion
#region Adimistradores
app.MapPost("/adiministradores/login", ([FromBody] LoginDTO loginDTO, iAdimistradorServico servico) =>
{
    if (servico.Login(loginDTO) != null)
    {
        return Results.Ok("Login realizado com sucesso!!!");
    }
    else
    {
        return Results.Unauthorized();
    }
}).WithTags("Adiministradores");
#endregion

#region Veiculos

app.MapPost("veiculos", ([FromBody] VeiculoDTO veiculoDTO, iVeiculoService service) =>
{
    var veiculo = new Veiculo
    {
        Nome = veiculoDTO.Nome,
        Ano = veiculoDTO.Ano,
        Marca = veiculoDTO.Marca
    };
    service.Incluir(veiculo);
    return Results.Created($"veiculo/{veiculo.Id}", veiculo);
}).WithTags("Veiculos");

app.MapGet("veiculos", ([FromQuery] int pagina, iVeiculoService service) =>
{
    var veiculos = service.Todos(pagina);
    return Results.Ok(veiculos);
}).WithTags("Veiculos");
app.MapGet("/veiculos/{id}", ([FromRoute] int id, iVeiculoService service) =>
{
    var veiculo = service.BuscaPorId(id);
    if (veiculo == null) return Results.NotFound();


    return Results.Ok(veiculo);
}).WithTags("Veiculos");

app.MapPut("/veiculos/{id}", ([FromRoute] int id, VeiculoDTO veiculoDTO, iVeiculoService service) =>
{
    var veiculo = service.BuscaPorId(id);
    if (veiculo == null) return Results.NotFound();
    veiculo.Nome = veiculoDTO.Nome;
    veiculo.Marca = veiculoDTO.Marca;
    veiculo.Ano = veiculoDTO.Ano;
    service.Atualizar(veiculo);
    return Results.Ok(veiculo);
}).WithTags("Veiculos");
app.MapDelete("/veiculos/{id}", ([FromRoute] int id, iVeiculoService service) =>
{
    var veiculo = service.BuscaPorId(id);
    if (veiculo == null) return Results.NotFound();
    service.Apagar(veiculo);
    return Results.Ok();
}).WithTags("Veiculos");
#endregion

#region Swagger
app.UseSwagger();
app.UseSwaggerUI();
#endregion
app.Run();