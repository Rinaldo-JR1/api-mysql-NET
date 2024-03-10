using minimal.Dominios.DTOS;

var builder = WebApplication.CreateBuilder(args);
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