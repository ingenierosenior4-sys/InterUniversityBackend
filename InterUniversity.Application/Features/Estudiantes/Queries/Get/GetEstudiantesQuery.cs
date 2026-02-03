using InterUniversity.Domain.Dtos;
using MediatR;
using UniversityApi.Features.Estudiantes.Queries.Get;

namespace InterUniversity.Application.Features.Estudiantes.Queries.Get;

public record struct GetEstudiantesQuery(GetEntityQuery Query) : IRequest<PagedResult<GetEstudiantesQueryResponse>>;
