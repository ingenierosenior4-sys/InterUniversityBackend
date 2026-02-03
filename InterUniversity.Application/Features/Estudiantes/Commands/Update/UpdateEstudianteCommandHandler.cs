using InterUniversity.Application.Abstractions.Context;
using InterUniversity.Application.Exceptions;
using InterUniversity.Domain.Abstractions;
using InterUniversity.Domain.Entities;
using InterUniversity.Domain.Repositories;
using MediatR;

namespace InterUniversity.Application.Features.Estudiantes.Commands.Update;

public class UpdateEstudianteCommandHandler(
    IUsuarioRepository usuarioRepository,
    IUnitOfWork unitOfWork,
    IContextAccessor contextAccessor) : IRequestHandler<UpdateEstudianteCommand>
{
    public async Task Handle(UpdateEstudianteCommand request, CancellationToken cancellationToken)
    {
        if (request.EstudianteId != int.Parse(contextAccessor.UserId))
            throw new ValidationException("No se pudo realizar la operación");

        if (await usuarioRepository.ExisteUsuarioEstudiante(request.NumeroIdentificacion, request.EstudianteId))
            throw new ValidationException("El número de identificación no es valido debido a que ya esta registrado");

        var estudiante = await usuarioRepository.ObtenerUsuarioEstudiante(request.EstudianteId)
            ?? throw new NotFoundException(nameof(Estudiante), request.EstudianteId);

        estudiante.NumeroIdentificacion = request.NumeroIdentificacion;
        estudiante.Nombres = request.Nombres;
        estudiante.Apellidos = request.Apellidos;
        estudiante.FechaNacimiento = request.FechaNacimiento;

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}