using System;
using Akka.Actor;
using akka_microservices_proj.Actors;
using akka_microservices_proj.Actors.Provider;
using akka_microservices_proj.Commands;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace akka_microservices_proj
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Adding swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BasketService", Version = "v1" });
            });

            // Creating actorsystem
            services.AddSingleton(x => ActorSystem.Create("BasketService"));
            services.AddSingleton<ActorProvider>();

            // Add Services
            services
                .AddScoped<IGetBasketFromCustomerCommand, GetBasketFromCustomerCommand>()
                .AddScoped(x => new Lazy<IGetBasketFromCustomerCommand>(
                    x.GetRequiredService<IGetBasketFromCustomerCommand>()));
            services
                .AddScoped<IAddProductToBasketCommand, AddProductToBasketCommand>()
                .AddScoped(x => new Lazy<IAddProductToBasketCommand>(
                    x.GetRequiredService<IAddProductToBasketCommand>()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Allow swagger in develop
                app.UseSwagger(); // Swagger-ui
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "BasketService V1");
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
