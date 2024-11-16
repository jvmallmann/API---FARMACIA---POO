using AVALIAÇÃO_A4.DataBase;
using Microsoft.EntityFrameworkCore;
using Serilog;

// Configuração de logs com Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(
        path: "Logs/log-.txt",
        rollingInterval: RollingInterval.Day,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
    )
    .MinimumLevel.Debug()
    .CreateLogger();

try
{
    Log.Information("Inicializando aplicação...");

    var builder = WebApplication.CreateBuilder(args);

    // Integrando o Serilog
    builder.Host.UseSerilog();

    // Configuração do banco de dados em memória
    builder.Services.AddDbContext<DbContextToMemory>(options =>
        options.UseInMemoryDatabase("DbFarmacia"));

    // Configuração de serviços essenciais
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // Configuração do ambiente
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "API de Farmácia V1");
            c.RoutePrefix = string.Empty; // Configura Swagger na raiz
        });
    }

    app.UseAuthorization();
    app.MapControllers();

    Log.Information("Aplicação iniciada com sucesso.");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "A aplicação falhou ao iniciar.");
}
finally
{
    Log.CloseAndFlush();
}
