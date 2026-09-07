using Amandaba.API.Application.Interfaces;
using Amandaba.API.Domain.Interfaces;
using Amandaba.Application.Interfaces;
using Amandaba.Application.UseCases;
using Amandaba.Domain.Interfaces;
using Amandaba.Infrastructure.Data;
using Amandaba.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Oracle.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Entity Framework + Oracle
builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseOracle(
        builder.Configuration.GetConnectionString("Oracle"),
        oracleOptions =>
        {
            oracleOptions.UseOracleSQLCompatibility(
                OracleSQLCompatibility.DatabaseVersion21
            );
        }
    );
});

// Repositories
builder.Services.AddTransient<IPetRepository, PetRepository>();
builder.Services.AddTransient<IEspecieRepository, EspecieRepository>();
builder.Services.AddTransient<IPesoRepository, PesoRepository>();
builder.Services.AddTransient<IVacinaRepository, VacinaRepository>();
builder.Services.AddTransient<IDoencaRepository, DoencaRepository>();
builder.Services.AddTransient<IAlergiaRepository, AlergiaRepository>();
builder.Services.AddTransient<IMedicamentoRepository, MedicamentoRepository>();
builder.Services.AddTransient<IConsultaRepository, ConsultaRepository>();
builder.Services.AddTransient<IExameRepository, ExameRepository>();

// UseCases
builder.Services.AddTransient<IPetUseCase, PetUseCase>();
builder.Services.AddTransient<IEspecieUseCase, EspecieUseCase>();
builder.Services.AddTransient<IPesoUseCase, PesoUseCase>();
builder.Services.AddTransient<IVacinaUseCase, VacinaUseCase>();
builder.Services.AddTransient<IDoencaUseCase, DoencaUseCase>();
builder.Services.AddTransient<IAlergiaUseCase, AlergiaUseCase>();
builder.Services.AddTransient<IMedicamentoUseCase, MedicamentoUseCase>();
builder.Services.AddTransient<IConsultaUseCase, ConsultaUseCase>();
builder.Services.AddTransient<IExameUseCase, ExameUseCase>();

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