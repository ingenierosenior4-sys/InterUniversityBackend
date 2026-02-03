using MediatR;
using Microsoft.Extensions.Logging;

namespace InterUniversity.Application.Abstractions.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(ILogger<TRequest> logger) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var name = request?.GetType().Name;

        try
        {
            logger.LogInformation("Ejecutando el comando {RequestName}", name);

            var result = await next();

            logger.LogInformation("El comando {RequestName} se ejecuto correctamente", name);

            return result;
        }
        catch (Exception exception)
        {
            if (logger.IsEnabled(LogLevel.Error))
            {
                logger.LogError(exception, "El comando {RequestName} tuvo errores", name);
            }
            throw;
        }
    }
}
