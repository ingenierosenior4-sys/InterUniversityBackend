using MediatR;

namespace InterUniversity.Application.Features.Estudiantes.Commands.Update;

public record struct UpdateEstudianteCommand(
    int EstudianteId,
    string NumeroIdentificacion,
    string Nombres,
    string Apellidos,
    DateOnly FechaNacimiento) : IRequest;
