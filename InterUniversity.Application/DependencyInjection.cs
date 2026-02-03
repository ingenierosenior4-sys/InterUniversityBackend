using InterUniversity.Application.Abstractions.Behaviors;
using InterUniversity.Application.Abstractions.Context;
using Microsoft.Extensions.DependencyInjection;

namespace InterUniversity.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddSingleton<IContextAccessor, ContextAccessor>();

        return services;
    }
}
