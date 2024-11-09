using Microsoft.EntityFrameworkCore;
using AVALIAÇÃO_A4.DataBase;


namespace AVALIAÇÃO_A4.DataBase.Models

{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Configuração do banco de dados InMemory
            services.AddDbContext<DbContextToMemory>(options =>
                options.UseInMemoryDatabase("DbFarmacia"));


            // Adiciona os controladores e configura o Swagger
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "API Farmácia", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            // Configuração do Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Farmácia V1");
                c.RoutePrefix = string.Empty;
            });

            // Configuração dos endpoints dos controladores
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
