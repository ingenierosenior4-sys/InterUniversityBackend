using InterUniversity.Application.Abstractions.Context;
using InterUniversity.Application.Exceptions;
using InterUniversity.Domain.Abstractions;
using InterUniversity.Domain.Entities;
using InterUniversity.Domain.Repositories;
using MediatR;

namespace InterUniversity.Application.Features.Estudiantes.Commands.Delete;

public class DeleteEstudianteCommandHandler(
    IUsuarioRepository usuarioRepository,
    IUnitOfWork unitOfWork,
    IContextAccessor contextAccessor) : IRequestHandler<DeleteEstudianteCommand>
{
    public async Task Handle(DeleteEstudianteCommand request, CancellationToken cancellationToken)
    {
        if (request.EstudianteId != int.Parse(contextAccessor.UserId))
            throw new ValidationException("No se pudo realizar la operación");

        var estudiante = await usuarioRepository.ObtenerUsuarioEstudiante(request.EstudianteId)
            ?? throw new NotFoundException(nameof(Estudiante), request.EstudianteId);

        usuarioRepository.Remove(estudiante);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}