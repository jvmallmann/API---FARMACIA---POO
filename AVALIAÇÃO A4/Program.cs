using AVALIA��O_A4.Classes;
using AVALIA��O_A4.DataBase;
using AVALIA��O_A4.Interface;
using AVALIA��O_A4.Repositorio;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Swagger configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// InMemoryDatabase configuration
builder.Services.AddDbContext<DbContextToMemory>(options => options.UseInMemoryDatabase("DbFarmacia"));

// Dependency Injection configuration
builder.Services.AddScoped<IRepository<Receita>, ReceitaRepositorio>(); // Registra o reposit�rio
builder.Services.AddScoped<IReceitaService, ReceitaService>(); // Registra o servi�o

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
