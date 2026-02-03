using AutoMapper;
using InterUniversity.Application.Mappings;
using InterUniversity.Domain.Dtos;
using InterUniversity.Domain.Entities;
using InterUniversity.Domain.Repositories;
using MediatR;
using UniversityApi.Features.Estudiantes.Queries.Get;

namespace InterUniversity.Application.Features.Estudiantes.Queries.Get;

public class GetEstudiantesQueryHandler(
    IEstudianteRepository estudianteRepository,
    IMapper mapper) : IRequestHandler<GetEstudiantesQuery, PagedResult<GetEstudiantesQueryResponse>>
{
    public async Task<PagedResult<GetEstudiantesQueryResponse>> Handle(GetEstudiantesQuery request, CancellationToken cancellationToken)
    {
        var paged = await estudianteRepository.ObtenerEstudiantesPaginado(request.Query.PageSize, request.Query.CurrentPage);

        return paged.MapTo<Estudiante, GetEstudiantesQueryResponse>(mapper);
    }
}
