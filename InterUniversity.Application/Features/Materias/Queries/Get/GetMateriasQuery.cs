using MediatR;

namespace InterUniversity.Application.Features.Materias.Queries.Get;

public record struct GetMateriasQuery() : IRequest<IEnumerable<GetMateriasQueryResponse>>;
