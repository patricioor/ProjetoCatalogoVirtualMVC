using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CleanArchMvc.Infra.IoC
{
    public static class DependencyInjectionJWT
    {
        public static IServiceCollection AddInfrastructureJWT(
            this IServiceCollection services, IConfiguration configuration)
        {
            // Inform the type of JWT-bearer authentication
            // Definy the model of challenge of authentication
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            // Enable the JWT authentication using the scheme of challenge defined
            // Validate the token
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                                           Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"])),
                    ClockSkew = TimeSpan.Zero
                };
            });
            return services;
        }   
    }
}
