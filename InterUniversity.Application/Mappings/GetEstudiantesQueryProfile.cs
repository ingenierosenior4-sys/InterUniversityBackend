using AutoMapper;
using InterUniversity.Domain.Entities;
using UniversityApi.Features.Estudiantes.Queries.Get;

namespace InterUniversity.Application.Mappings;

public class GetEstudiantesQueryProfile : Profile
{
    public GetEstudiantesQueryProfile() =>
        CreateMap<Estudiante, GetEstudiantesQueryResponse>()
            .ForMember(dest =>
                dest.NumeroIdentificacion,
                opt => opt.MapFrom(mf => mf.EstudianteNavigation.NumeroIdentificacion))
            .ForMember(dest =>
                dest.Nombres,
                opt => opt.MapFrom(mf => mf.EstudianteNavigation.Nombres))
            .ForMember(dest =>
                dest.Apellidos,
                opt => opt.MapFrom(mf => mf.EstudianteNavigation.Apellidos));
}
