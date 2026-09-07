using Amandaba.Application.Interfaces;
using Amandaba.Application.UseCases;
using Amandaba.Domain.Interfaces;
using Amandaba.Infrastructure.Data;
using Amandaba.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Entity Framework + Oracle
builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseOracle(builder.Configuration.GetConnectionString("Oracle"));
});

// Repositories
builder.Services.AddTransient<IPetRepository, PetRepository>();
builder.Services.AddTransient<IEspecieRepository, EspecieRepository>();
builder.Services.AddTransient<IPesoRepository, PesoRepository>();

// UseCases
builder.Services.AddTransient<IPetUseCase, PetUseCase>();
builder.Services.AddTransient<IEspecieUseCase, EspecieUseCase>();
builder.Services.AddTransient<IPesoUseCase, PesoUseCase>();

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
});

var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();