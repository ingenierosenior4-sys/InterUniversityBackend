using MediatR;

namespace InterUniversity.Application.Features.Clases.Queries.Get;

public record struct GetClasesQuery(int EstudianteId) : IRequest<IEnumerable<GetClasesQueryResponse>>;
