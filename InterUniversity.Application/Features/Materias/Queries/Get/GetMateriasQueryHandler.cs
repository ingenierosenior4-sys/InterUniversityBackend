using AutoMapper;
using InterUniversity.Domain.Repositories;
using MediatR;

namespace InterUniversity.Application.Features.Materias.Queries.Get;

public class GetMateriasQueryHandler(
    IMateriaProfesorRepository materiaProfesorRepository,
    IMapper mapper) : IRequestHandler<GetMateriasQuery, IEnumerable<GetMateriasQueryResponse>>
{
    public async Task<IEnumerable<GetMateriasQueryResponse>> Handle(GetMateriasQuery request, CancellationToken cancellationToken)
    {
        var materias = await materiaProfesorRepository.ObtenerMaterias(cancellationToken);

        return mapper.Map<IEnumerable<GetMateriasQueryResponse>>(materias);
    }
}

