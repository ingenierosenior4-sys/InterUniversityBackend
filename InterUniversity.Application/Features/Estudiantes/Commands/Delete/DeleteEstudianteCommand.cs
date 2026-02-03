using MediatR;

namespace InterUniversity.Application.Features.Estudiantes.Commands.Delete;

public record struct DeleteEstudianteCommand(int EstudianteId) : IRequest;
