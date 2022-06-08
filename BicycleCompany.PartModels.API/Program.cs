using BicycleCompany.PartModels.API.Extensions;
using BicycleCompany.PartModels.API.Extensions.Mapping;
using BicycleCompany.PartModels.API.Extensions.Utils;
using Microsoft.AspNetCore.Mvc;
using NLog;

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigurationManager configuration = builder.Configuration;

builder.Services.ConfigureSqlContext(configuration);
builder.Services.ConfigureLoggerService();
builder.Services.AddAutoMapper(typeof(MappingProfiles));
builder.Services.RegisterRepositories();
builder.Services.RegisterServices();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureCorsPolicy(configuration);
builder.Services.ConfigureSwagger();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.UseMiddleware<ExceptionHandler>();

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
