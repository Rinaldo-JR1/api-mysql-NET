using Microsoft.EntityFrameworkCore;
using minimal.Dominios.DTOS;
using minimal.Dominios.Interfaces;
using minimal.Infraestrutura.DB;
using minimal.Dominios.Servicos;
using Microsoft.AspNetCore.Mvc;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DbContexto>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("mysql"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("mysql")));
});
builder.Services.AddScoped<iAdimistradorServico, AdiminstradorService>();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("login", ([FromBody] LoginDTO loginDTO, iAdimistradorServico servico) =>
{
    if (servico.Login(loginDTO) != null)
    {
        return Results.Ok("Login realizado com sucesso!!!");
    }
    else
    {
        return Results.Unauthorized();
    }
});


app.Run();