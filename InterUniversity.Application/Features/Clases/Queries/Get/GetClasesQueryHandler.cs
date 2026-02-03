using AutoMapper;
using InterUniversity.Domain.Repositories;
using MediatR;

namespace InterUniversity.Application.Features.Clases.Queries.Get;

public class GetClasesQueryHandler(
    IClaseRepository claseRepository,
    IMapper mapper) : IRequestHandler<GetClasesQuery, IEnumerable<GetClasesQueryResponse>>
{
    public async Task<IEnumerable<GetClasesQueryResponse>> Handle(GetClasesQuery request, CancellationToken cancellationToken)
    {
        var clasesEstudiante = await claseRepository.ObtenerClasesEstudiante(request.EstudianteId, cancellationToken);

        return mapper.Map<IEnumerable<GetClasesQueryResponse>>(clasesEstudiante);
    }
}