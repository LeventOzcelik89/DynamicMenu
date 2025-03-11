using DynamicMenu.Core.Interfaces;
using DynamicMenu.Core.Models;
using DynamicMenu.Infrastructure.Auth;
using DynamicMenu.Infrastructure.Cache;
using DynamicMenu.Infrastructure.Data;
using DynamicMenu.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DynamicMenu.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {

            // DbContext
            services.AddDbContext<DynamicMenuDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("DynamicMenu.Infrastructure")
                ), ServiceLifetime.Scoped); // Scoped lifetime eklendi
            
            // Redis yerine In-Memory Cache kullanacağız
            // services.AddStackExchangeRedisCache(options =>
            // {
            //     options.Configuration = configuration.GetConnectionString("Redis");
            //     options.InstanceName = "DynamicMenu_";
            // });

            // JWT Authentication
            var authSettings = configuration.GetSection("AuthSettings").Get<AuthSettings>();
            services.Configure<AuthSettings>(configuration.GetSection("AuthSettings"));
            
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authSettings.Secret)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = authSettings.Issuer,
                    ValidAudience = authSettings.Audience
                };
            });

            // Services
            services.AddScoped<IMenuItemRepository, MenuItemRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IRemoteMenusRepository, RemoteMenuConfigRepository>();
            services.AddScoped<IRemoteMenusRepository, RemoteMenusRepository>();
            services.AddScoped<ICacheService, InMemoryCacheService>();
            services.AddScoped<JwtService>();

            return services;
        }
    }
} 