namespace InterUniversity.Api.Extensions;

public static class CorsExtension
{
    public static IServiceCollection AddCorsExtension(this IServiceCollection services, IConfiguration configuration, string specificOrigins)
    {
        var corsOrigins = configuration.GetSection("CorsOrigins").Get<string[]>();

        services.AddCors(options => options
            .AddPolicy(specificOrigins, builder => builder
            .WithOrigins(corsOrigins!)
            .AllowAnyHeader()
            .AllowAnyMethod())
        );

        return services;
    }
}
