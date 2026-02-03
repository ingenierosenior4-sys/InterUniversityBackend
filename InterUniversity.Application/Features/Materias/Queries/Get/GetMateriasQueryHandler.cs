using AutoMapper;
using AutoMapper.QueryableExtensions;
using InterUniversity.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InterUniversity.Application.Features.Materias.Queries.Get;

public class GetMateriasQueryHandler(
    IMateriaProfesorRepository materiaProfesorRepository,
    IMapper mapper) : IRequestHandler<GetMateriasQuery, IEnumerable<GetMateriasQueryResponse>>
{
    public async Task<IEnumerable<GetMateriasQueryResponse>> Handle(GetMateriasQuery request, CancellationToken cancellationToken)
    {
        return await materiaProfesorRepository.ObtenerMaterias()
            .ProjectTo<GetMateriasQueryResponse>(mapper.ConfigurationProvider)
            .ToListAsync();
    }
}

