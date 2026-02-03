using InterUniversity.Application.Abstractions.Context;
using InterUniversity.Domain.Abstractions;
using InterUniversity.Domain.Entities;
using InterUniversity.Domain.Repositories;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace InterUniversity.Application.Features.Clases.Commands.Create;

public class CreateClaseCommandHandler(
    IMateriaRepository materiaRepository,
    IEstudianteRepository estudianteRepository,
    IClaseRepository claseRepository,
    IUnitOfWork unitOfWork,
    IContextAccessor contextAccessor) : IRequestHandler<CreateClaseCommand>
{

    public async Task Handle(CreateClaseCommand request, CancellationToken cancellationToken)
    {
        var idsMaterias = request.Clases.Select(c => c.MateriaId);
        var creditosMaterias = materiaRepository.ObtenerSumaCreditosPorMaterias(idsMaterias);

        var estudiante = await estudianteRepository.FindAsync(int.Parse(contextAccessor.UserId)) ?? new Estudiante();

        if (creditosMaterias > estudiante.Creditos)
            throw new ValidationException("No cuenta con los creditos suficientes para registrar materias");

        var clases = request.Clases.Select(c => new Clase
        {
            ProfesorId = c.ProfesorId,
            MateriaId = c.MateriaId,
            EstudianteId = int.Parse(contextAccessor.UserId)
        }).ToList();

        estudiante.Creditos -= (byte)creditosMaterias;

        claseRepository.AddRange(clases);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}