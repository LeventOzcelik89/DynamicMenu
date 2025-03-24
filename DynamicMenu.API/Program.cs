using DynamicMenu.Infrastructure.Extensions;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using DynamicMenu.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using DynamicMenu.Core.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    })
    .AddApplicationPart(typeof(Program).Assembly);

var appSettings = new AppSettings();
builder.Configuration.GetSection(nameof(AppSettings)).Bind(appSettings);

builder.Services.AddSingleton(appSettings);

// Add Infrastructure services (DbContext, Redis, JWT, etc.)
builder.Services.AddInfrastructureServices(builder.Configuration);

// Add CORS if needed
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Dynamic Menu API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Redis Cache yerine In-Memory Cache kullanacağız
builder.Services.AddMemoryCache();

// DbContext
builder.Services.AddDbContext<DynamicMenuDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("DynamicMenu.Infrastructure")  // Migration assembly'i belirt
    ));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dynamic Menu API V1");
        c.RoutePrefix = "swagger";
    });
}

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();