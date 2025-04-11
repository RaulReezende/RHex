using RHex.Application.Features.Ferramenta;
using RHex.Infrastructure.Data;
using RHex.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using RHex.Domain.Interfaces;
using RHex.Application.Features.Ferramenta.DTOs;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("RHex.Infrastructure")
    ));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IFerramentaRepository, FerramentaRepository>();
builder.Services.AddScoped<FerramentaService>();

builder.Services.AddProblemDetails();
builder.Services.AddValidatorsFromAssemblyContaining<CreateFerramentaDto>(includeInternalTypes: true);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/ferramentas", async (FerramentaService service) =>
    Results.Ok(await service.GetAllFerramentas()));

app.MapGet("/ferramentas/{id}", async (Guid id, FerramentaService service) =>
{
    var ferramenta = await service.GetFerramentaById(id);
    return ferramenta is null ? Results.NotFound() : Results.Ok(ferramenta);
});

app.MapPost("/ferramentas", async (
    CreateFerramentaDto dto, 
    FerramentaService service,
    IValidator<CreateFerramentaDto> validator
) =>
{
    var validationResult = await validator.ValidateAsync(dto);
    if (!validationResult.IsValid)
    {
        return Results.ValidationProblem(validationResult.ToDictionary());
    }

    var ferramenta = await service.CreateFerramenta(dto);
    return Results.Created($"/ferramentas/{ferramenta.Id}", ferramenta);
});

app.MapPut("/ferramentas/{id}", async (
    Guid id,
    UpdateFerramentaDto dto,
    FerramentaService service,
    IValidator<UpdateFerramentaDto> validator
) =>
{
    var validationResult = await validator.ValidateAsync(dto);
    if (!validationResult.IsValid)
    {
        return Results.ValidationProblem(validationResult.ToDictionary());
    }

    var ferramenta = await service.UpdateFerramenta(id, dto);
    return ferramenta is null ? Results.NotFound() : Results.Ok(ferramenta);
});

app.MapDelete("/ferramentas/{id}", async (Guid id, FerramentaService service) =>
    await service.DeleteFerramenta(id) ? Results.NoContent() : Results.NotFound());

app.Run();