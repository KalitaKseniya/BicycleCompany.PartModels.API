using BicycleCompany.PartModels.API.Infrastructure;
using BicycleCompany.PartModels.API.Repositories;
using BicycleCompany.PartModels.API.Repositories.Interfaces;
using BicycleCompany.PartModels.API.Services;
using BicycleCompany.PartModels.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace BicycleCompany.PartModels.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<RepositoryContext>(opts =>
                opts.UseSqlServer(configuration.GetConnectionString("sqlConnection")));

        public static void ConfigureLoggerService(this IServiceCollection services) =>
            services.AddScoped<ILoggerManager, LoggerManager>();

        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IPartRepository, PartRepository>();
            services.AddScoped<IPartDetailsRepository, PartDetailsRepository>();
            services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
        }

        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IPartService, PartService>();
            services.AddScoped<IManufacturerService, ManufacturerService>();
        }

        public static void ConfigureCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        public static void ConfigureSwagger(this IServiceCollection services) =>
            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "BicycleCompany API",
                    Description = "A simple ASP.NET Core Web API",
                });
                //ToDo: do we need auth?
                //c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                //{
                //    In = ParameterLocation.Header,
                //    Description = "Place to add JWT with Bearer",
                //    Name = "Authorization",
                //    Type = SecuritySchemeType.ApiKey,
                //    Scheme = "Bearer"
                //});

                //c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                //{
                //    {
                //        new OpenApiSecurityScheme
                //        {
                //            Reference = new OpenApiReference
                //            {
                //                Type = ReferenceType.SecurityScheme,
                //                Id = "Bearer"
                //            },
                //            Name = "Bearer"
                //        },
                //        new List<string>()
                //    }
                //});

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
    }
}
