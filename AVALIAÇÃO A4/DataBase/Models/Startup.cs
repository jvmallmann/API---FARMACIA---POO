using AVALIAÇÃO_A4.Interface;
using AVALIAÇÃO_A4.Service;
using AVALIAÇÃO_A4.Validate;
using AVALIAÇÃO_A4.DataBase.Models;
using AVALIAÇÃO_A4.DataBase;
using AVALIAÇÃO_A4.Repository;
using Microsoft.EntityFrameworkCore;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<DbContextToMemory>(options =>
            options.UseInMemoryDatabase("DbFarmacia"));

        services.AddControllers();

        // Registro do AutoMapper
        services.AddAutoMapper(typeof(Startup)); // Isso registra o IMapper para injeção de dependência

        // Registro de serviços e repositórios
        services.AddScoped<IVendaService, VendaService>();
        services.AddScoped<IClienteService, ClienteService>();
        services.AddScoped<IReceitaService, ReceitaService>();
        services.AddScoped<IRemedioService, RemedioService>();

        // Registro de repositórios
        services.AddScoped<IRepository<Venda>, VendaRepository>();
        services.AddScoped<IRepository<Cliente>, ClienteRepository>();
        services.AddScoped<IRepository<Receita>, ReceitaRepository>();
        services.AddScoped<IRepository<Remedio>, RemedioRepository>();

        // Registro de validadores (usando Scoped para permitir o uso de DbContext)
        services.AddScoped<VendaValidator>();
        services.AddScoped<ClienteValidator>();
        services.AddScoped<ReceitaValidator>();
        services.AddScoped<RemedioValidator>();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "API de Farmácia", Version = "v1" });
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "API de Farmácia V1");
            c.RoutePrefix = string.Empty;
        });

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
