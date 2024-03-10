using Microsoft.EntityFrameworkCore;
using minimal.Dominios.DTOS;
using minimal.Infraestrutura.DB;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DbContexto>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("mysql"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("mysql")));
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("login", (LoginDTO loginDTO) =>
{
    if (loginDTO.Senha == "1234" && loginDTO.Email == "rinaldo3uchoa@gmail.com")
    {
        return Results.Ok("Login realizado com sucesso!");
    }
    else
    {
        return Results.Unauthorized();
    }
});


app.Run();