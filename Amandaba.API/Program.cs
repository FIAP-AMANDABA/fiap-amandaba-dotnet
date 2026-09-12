using Amandaba.API.Application.Interfaces;
using Amandaba.API.Application.UseCases;
using Amandaba.API.Domain.Interfaces;
using Amandaba.Application.Interfaces;
using Amandaba.Application.UseCases;
using Amandaba.Domain.Interfaces;
using Amandaba.Infrastructure.Data;
using Amandaba.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using Oracle.EntityFrameworkCore;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override(
        "Microsoft.AspNetCore",
        LogEventLevel.Warning
    )
    .Enrich.FromLogContext()
    .WriteTo.Console(
        outputTemplate:
            "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} " +
            "[{Level:u3}] " +
            "[TraceId:{TraceId}] " +
            "[SpanId:{SpanId}] " +
            "{SourceContext}{NewLine}" +
            "    {Message:lj}{NewLine}{Exception}"
    )
    .WriteTo.File(
        path: Path.Combine(
            AppContext.BaseDirectory,
            "logs",
            "api-.log"
        ),
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 7,
        outputTemplate:
            "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} " +
            "[{Level:u3}] " +
            "[TraceId:{TraceId}] " +
            "[SpanId:{SpanId}] " +
            "{SourceContext}{NewLine}" +
            "    {Message:lj}{NewLine}{Exception}"
    )
    .CreateLogger();

builder.Services.AddSerilog();

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

// Health Checks
builder.Services.AddHealthChecks()
    .AddCheck(
        "self",
        () => HealthCheckResult.Healthy(),
        tags: ["live"])
    .AddOracle(
        connectionString:
            builder.Configuration.GetConnectionString("Oracle") ?? "",
        name: "oracle",
        failureStatus: HealthStatus.Unhealthy,
        tags: ["db"]);

// OpenTelemetry
builder.Services.AddOpenTelemetry()
    .WithTracing(tracing =>
    {
        tracing
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddConsoleExporter();
    })
    .WithMetrics(metrics =>
    {
        metrics
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddConsoleExporter();
    });
    
builder.Services.AddHttpClient<GeminiUseCase>();

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
});

var app = builder.Build();
app.UseCors("AllowAll");

//CORS

app.UseCors(policy =>
{
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader();
});

// Swagger
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Health Check - Liveness
app.MapHealthChecks("/health/live", new HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("live")
});

// Health Check - Readiness / Oracle
app.MapHealthChecks("/health/db", new HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("db")
});

app.Run();

public partial class Program { }