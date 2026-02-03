using AutoMapper;
using InterUniversity.Domain.Entities;
using UniversityApi.Features.Estudiantes.Queries.GetEstudiante;

namespace InterUniversity.Application.Mappings;

public class GetEstudianteQueryProfile : Profile
{
    public GetEstudianteQueryProfile() =>
        CreateMap<Estudiante, GetEstudianteQueryResponse>()
            .ForMember(dest =>
                dest.NumeroIdentificacion,
                opt => opt.MapFrom(mf => mf.EstudianteNavigation.NumeroIdentificacion))
            .ForMember(dest =>
                dest.Nombres,
                opt => opt.MapFrom(mf => mf.EstudianteNavigation.Nombres))
            .ForMember(dest =>
                dest.Apellidos,
                opt => opt.MapFrom(mf => mf.EstudianteNavigation.Apellidos))
            .ForMember(dest =>
                dest.FechaNacimiento,
                opt => opt.MapFrom(mf => mf.EstudianteNavigation.FechaNacimiento));

}