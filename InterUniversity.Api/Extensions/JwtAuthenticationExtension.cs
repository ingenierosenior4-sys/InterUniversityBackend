using InterUniversity.Application.Abstractions.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace InterUniversity.Api.Extensions;

public static class JwtAuthenticationExtension
{
    public static IServiceCollection AddJwtAuthenticationExtension(this IServiceCollection services, IConfiguration configuration)
    {
        // Añadir autentificación por JWT
        var jwtOptions = configuration.GetSection(nameof(JWTOptions));

        services.Configure<JWTOptions>(options =>
        {
            options.Issuer = jwtOptions[nameof(JWTOptions.Issuer)]!;
            options.Audience = jwtOptions[nameof(JWTOptions.Audience)]!;
            options.ValidForMinutes = int.Parse(jwtOptions[nameof(JWTOptions.ValidForMinutes)]!);
            // Use a HMAC SHA algorithm that is compatible with JWT (HmacSha256)
            options.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["SecretKeyJWT"]!)), SecurityAlgorithms.HmacSha256);
        });

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtOptions[nameof(JWTOptions.Issuer)],
                    ValidateAudience = true,
                    ValidAudience = jwtOptions[nameof(JWTOptions.Audience)],
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SecretKeyJWT"]!)),
                    ClockSkew = TimeSpan.Zero
                };
            });

        services.AddScoped<IJWTFactory, JWTFactory>();

        return services;
    }
}
