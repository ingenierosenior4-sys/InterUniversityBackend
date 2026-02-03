using MediatR;

namespace InterUniversity.Application.Features.Clases.Queries.GetClase;

public record struct GetClaseQuery(int ProfesorId, int MateriaId) : IRequest<GetClaseQueryResponse>;
