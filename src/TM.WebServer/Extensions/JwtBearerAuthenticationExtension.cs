using System.Text;
using TM.Application.Services;
using TM.WebServer.Entities;
using TM.WebServer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace TM.WebServer.Extensions
{
    public static class JwtBearerAuthenticationExtension
    {
        public static IServiceCollection AddJwtBearerAuthentication(this IServiceCollection services,
           IConfiguration configuration)
        {
            services.Configure<JwtToken>(configuration.GetSection("JwtToken"));

            services.AddAuthentication(config =>
                {
                    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = configuration["JwtToken:ValidIssuer"],
                        ValidAudience = configuration["JwtToken:ValidAudience"],
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtToken:Secret"]))
                    };
                });

            services.AddSingleton<IJwtTokenService, JwtTokenService>();
            services.AddScoped<Application.Interfaces.IAuthorizationService, AuthorizationService>();

            return services;
        }
    }
}
