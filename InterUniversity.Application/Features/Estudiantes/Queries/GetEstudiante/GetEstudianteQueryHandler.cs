using AutoMapper;
using InterUniversity.Application.Exceptions;
using InterUniversity.Domain.Entities;
using InterUniversity.Domain.Repositories;
using MediatR;

namespace UniversityApi.Features.Estudiantes.Queries.GetEstudiante;

public class GetEstudianteQueryHandler(
    IEstudianteRepository estudianteRepository,
    IMapper mapper) : IRequestHandler<GetEstudianteQuery, GetEstudianteQueryResponse>
{
    public async Task<GetEstudianteQueryResponse> Handle(GetEstudianteQuery request, CancellationToken cancellationToken)
    {
        var estudiante = await estudianteRepository.ObtenerEstudiante(request.EstudianteId)
            ?? throw new NotFoundException(nameof(Estudiante), request.EstudianteId);

        return mapper.Map<GetEstudianteQueryResponse>(estudiante);
    }
}
