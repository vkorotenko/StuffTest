using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using StuffTest;
using StuffTest.Data;
using StuffTest.Data.Abstract;
using StuffTest.Data.Repositories;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var services = builder.Services;
var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()));
services.AddSingleton(mappingConfig.CreateMapper());
services
            
            .AddDbContext<StuffContent>(options =>
                options.UseSqlite(
                    "Data Source=mydb.db;",
                    o => o.MigrationsAssembly("StuffTest")
                ).EnableSensitiveDataLogging()
            );


services.AddScoped<IUserRepository, UserRepository>();
services.AddScoped<IPositionRepository, PositionRepository>();


services
    .AddMvc()
    .AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

});
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "StuffTest API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,

                    },
                    new List<string>()
                }
            });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});



var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();
app.UseSwagger();
IApplicationBuilder applicationBuilder = app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("v1/swagger.json", "StuffTest API V1");
});

var scope = app.Services.CreateScope();

var ctx = scope.ServiceProvider.GetRequiredService<StuffContent>();
ctx.Database.EnsureCreated();


app.Run();
