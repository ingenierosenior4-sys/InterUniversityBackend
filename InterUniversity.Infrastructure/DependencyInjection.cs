using InterUniversity.Domain.Abstractions;
using InterUniversity.Domain.Repositories;
using InterUniversity.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InterUniversity.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
      this IServiceCollection services,
      IConfiguration configuration
      )
    {
        var connectionString = configuration.GetConnectionString("Database")
             ?? throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<UniversidadDbContext>(options => options.UseSqlServer(connectionString));

        services.AddScoped<IClaseRepository, ClaseRepository>();
        services.AddScoped<ICreditoRepository, CreditoRepository>();
        services.AddScoped<IEstudianteRepository, EstudianteRepository>();
        services.AddScoped<IMateriaProfesorRepository, MateriaProfesorRepository>();
        services.AddScoped<IMateriaRepository, MateriaRepository>();
        services.AddScoped<IProfesorRepository, ProfesorRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<UniversidadDbContext>());

        return services;
    }
}
