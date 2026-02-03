using AutoMapper;
using AutoMapper.QueryableExtensions;
using InterUniversity.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InterUniversity.Application.Features.Clases.Queries.Get;

public class GetClasesQueryHandler(
    IClaseRepository claseRepository,
    IMapper mapper) : IRequestHandler<GetClasesQuery, IEnumerable<GetClasesQueryResponse>>
{
    public async Task<IEnumerable<GetClasesQueryResponse>> Handle(GetClasesQuery request, CancellationToken cancellationToken)
    {
        return await claseRepository.ObtenerClasesEstudiante(request.EstudianteId)
            .ProjectTo<GetClasesQueryResponse>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}