using AutoMapper;
using InterUniversity.Application.Features.Clases.Queries.GetClase;
using InterUniversity.Domain.Entities;

namespace InterUniversity.Application.Mappings;

public class GetClaseQueryProfile : Profile
{
    public GetClaseQueryProfile() =>
        CreateMap<MateriaProfesor, GetClaseQueryResponse>()
             .ForMember(dest =>
                dest.Titulo,
                opt => opt.MapFrom(mf => mf.Materia.Titulo))
            .ForMember(dest =>
                dest.Profesor,
                opt => opt.MapFrom(mf => $"{mf.Profesor.ProfesorNavigation.Nombres} {mf.Profesor.ProfesorNavigation.Apellidos}"));
}
