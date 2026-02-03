using InterUniversity.Application.Abstractions.Context;
using InterUniversity.Domain.Abstractions;
using InterUniversity.Domain.Entities;
using InterUniversity.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace InterUniversity.Application.Features.Estudiantes.Commands.Create;

public class CreateEstudianteCommandHandler(
    IEstudianteRepository estudianteRepository,
    ICreditoRepository creditoRepository,
    IUnitOfWork unitOfWork,
    IContextAccessor contextAccessor,
    IConfiguration configuration) : IRequestHandler<CreateEstudianteCommand>
{
    public async Task Handle(CreateEstudianteCommand request, CancellationToken cancellationToken)
    {
        var idCredito = configuration.GetValue<int>("Parametros:CreditoIdEstudianteNuevo");

        var credito = await creditoRepository.FindAsync(idCredito) ?? new Credito();

        var estudiante = new Estudiante
        {
            EstudianteId = int.Parse(contextAccessor.UserId),
            FechaInscrito = DateTime.Now,
            Creditos = credito.Creditos
        };

        estudianteRepository.Add(estudiante);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}