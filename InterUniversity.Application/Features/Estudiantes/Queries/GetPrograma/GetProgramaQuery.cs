using MediatR;
using UniversityApi.Features.Estudiantes.Queries.GetPrograma;

namespace InterUniversity.Application.Features.Estudiantes.Queries.GetPrograma;

public record struct GetProgramaQuery() : IRequest<GetProgramaQueryResponse>;
