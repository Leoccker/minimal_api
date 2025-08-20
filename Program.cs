using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using minimal_api.Dominio.Interfaces;
using minimal_api.Dominio.Serviços;
using minimal_api.DTOs;
using minimal_api.Infraestrutura.DB;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IADMService, ADMService>();

builder.Services.AddDbContext<DBContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("mysql"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("mysql"))));
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("/login", async ([FromBody] LoginDTO loginDTO, IADMService admService) =>
{
    if (admService.Login(loginDTO) is not null)
    {
        return Results.Ok("Login successful");
    }
    return Results.Unauthorized();
}); 

app.Run();

