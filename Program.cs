using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using minimal_api.Dominio.Entidades;
using minimal_api.Dominio.Interfaces;
using minimal_api.Dominio.ModelViews;
using minimal_api.Dominio.Serviços;
using minimal_api.DTOs;
using minimal_api.Infraestrutura.DB;


#region Builder
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IADMService, ADMService>();
builder.Services.AddScoped<IVeiculosService, VeiculosService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DBContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("mysql"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("mysql"))));

var app = builder.Build();
#endregion


#region Home
app.MapGet("/", () => Results.Json(new Home()));
#endregion

#region ADM
app.MapPost("adm/login", ([FromBody] LoginDTO loginDTO, IADMService admService) =>
{
    if (admService.Login(loginDTO) is not null)
    {
        return Results.Ok("Login successful");
    }
    return Results.Unauthorized();
});
#endregion

#region Veiculos
app.MapPost("/veiculos", ([FromBody] VeiculoDTO veiculoDTO, IVeiculosService veiculoService) =>
{
    var veiculo = new Veiculo
    {
        Nome = veiculoDTO.Nome,
        Marca = veiculoDTO.Marca,
        Ano = veiculoDTO.Ano
    };
    veiculoService.Incluir(veiculo);
    return Results.Created($"/veiculos/{veiculo.Id}", veiculo);
});

app.MapGet("/veiculos", ([FromQuery] int? pagina, IVeiculosService veiculoService) =>
{
    var veiculos = veiculoService.Todos(pagina);
    return Results.Ok(veiculos);
}).WithTags("Veiculos");

app.MapGet("/veiculos/{id}", ([FromRoute] int id, IVeiculosService veiculoService) =>
{
    var veiculo = veiculoService.BuscaPorId(id);
    if (veiculo == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(veiculo);
}).WithTags("Veiculos");

app.MapPut("/veiculos/{id}", ([FromRoute] int id, VeiculoDTO veiculoDTO , IVeiculosService veiculoService) =>
{
    var veiculo = veiculoService.BuscaPorId(id);
    if (veiculo == null)
    {
        return Results.NotFound();
    }
    veiculo.Nome = veiculoDTO.Nome;
    veiculo.Marca = veiculoDTO.Marca;
    veiculo.Ano = veiculoDTO.Ano;
    veiculoService.Atualizar(id, veiculo);
    return Results.Ok(veiculo);
}).WithTags("Veiculos");


app.MapDelete("/veiculos/{id}", ([FromRoute] int id, IVeiculosService veiculoService) =>
{
    var veiculo = veiculoService.BuscaPorId(id);
    if (veiculo == null)
    {
        return Results.NotFound();
    }

    veiculoService.Apagar(veiculo);

    return Results.NoContent();
}).WithTags("Veiculos");
#endregion

app.UseSwagger();
app.UseSwaggerUI();
app.Run();

