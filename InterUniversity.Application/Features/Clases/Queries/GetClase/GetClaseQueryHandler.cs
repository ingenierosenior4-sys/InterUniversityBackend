using AutoMapper;
using InterUniversity.Application.Exceptions;
using InterUniversity.Domain.Repositories;
using MediatR;

namespace InterUniversity.Application.Features.Clases.Queries.GetClase;

public class GetClaseQueryHandler(
    IMateriaProfesorRepository materiaProfesorRepository,
    IClaseRepository claseRepository,
    IMapper mapper) : IRequestHandler<GetClaseQuery, GetClaseQueryResponse>
{
    public async Task<GetClaseQueryResponse> Handle(GetClaseQuery request, CancellationToken cancellationToken)
    {
        var clase = await materiaProfesorRepository
            .ObtenerClaseEstudiante(request.MateriaId, request.ProfesorId, cancellationToken) ?? throw new NotFoundException("No se encontro la clase");

        var response = mapper.Map<GetClaseQueryResponse>(clase);

        response.Estudiantes = await claseRepository.ObtenerEstudiantes(request.MateriaId, request.ProfesorId, cancellationToken);


        return response!;
    }
}
