using AutoMapper;
using InterUniversity.Application.Features.Materias.Queries.Get;
using InterUniversity.Domain.Entities;

namespace InterUniversity.Application.Mappings;

public class GetMateriasQueryProfile : Profile
{
    public GetMateriasQueryProfile() =>
        CreateMap<MateriaProfesor, GetMateriasQueryResponse>()
             .ForMember(dest =>
                dest.Titulo,
                opt => opt.MapFrom(mf => mf.Materia.Titulo))
             .ForMember(dest =>
                dest.Creditos,
                opt => opt.MapFrom(mf => mf.Materia.Creditos))
            .ForMember(dest =>
                dest.Profesor,
                opt => opt.MapFrom(mf => $"{mf.Profesor.ProfesorNavigation.Nombres} {mf.Profesor.ProfesorNavigation.Apellidos}"));
}
