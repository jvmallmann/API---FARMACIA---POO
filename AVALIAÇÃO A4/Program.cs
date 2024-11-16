using AVALIA��O_A4.DataBase;
using Microsoft.EntityFrameworkCore;
using Serilog;

// Configura��o de logs com Serilog
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
    Log.Information("Inicializando aplica��o...");

    var builder = WebApplication.CreateBuilder(args);

    // Integrando o Serilog
    builder.Host.UseSerilog();

    // Configura��o do banco de dados em mem�ria
    builder.Services.AddDbContext<DbContextToMemory>(options =>
        options.UseInMemoryDatabase("DbFarmacia"));

    // Configura��o de servi�os essenciais
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // Configura��o do ambiente
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "API de Farm�cia V1");
            c.RoutePrefix = string.Empty; // Configura Swagger na raiz
        });
    }

    app.UseAuthorization();
    app.MapControllers();

    Log.Information("Aplica��o iniciada com sucesso.");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "A aplica��o falhou ao iniciar.");
}
finally
{
    Log.CloseAndFlush();
}
