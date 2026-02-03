using AutoMapper;
using AutoMapper.QueryableExtensions;
using InterUniversity.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InterUniversity.Application.Features.Clases.Queries.GetClase;

public class GetClaseQueryHandler(
    IMateriaProfesorRepository materiaProfesorRepository,
    IClaseRepository claseRepository,
    IMapper mapper) : IRequestHandler<GetClaseQuery, GetClaseQueryResponse>
{
    public async Task<GetClaseQueryResponse> Handle(GetClaseQuery request, CancellationToken cancellationToken)
    {
        var response = await materiaProfesorRepository.ObtenerClaseEstudiante(request.MateriaId, request.ProfesorId)
            .ProjectTo<GetClaseQueryResponse>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (response != null)
        {
            response.Estudiantes = await claseRepository.ObtenerClaseEstudiantes(request.MateriaId, request.ProfesorId)
                .Select(m => $"{m.Estudiante.EstudianteNavigation.Nombres} {m.Estudiante.EstudianteNavigation.Apellidos}")
                .ToArrayAsync(cancellationToken);
        }

        return response!;
    }
}
