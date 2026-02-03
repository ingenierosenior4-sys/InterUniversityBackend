using AutoMapper;
using InterUniversity.Application.Features.Clases.Queries.Get;
using InterUniversity.Domain.Entities;

namespace InterUniversity.Application.Mappings;

public class GetClasesQueryProfile : Profile
{
    public GetClasesQueryProfile() =>
        CreateMap<Clase, GetClasesQueryResponse>()
             .ForMember(dest =>
                dest.Titulo,
                opt => opt.MapFrom(mf => mf.MateriaProfesor.Materia.Titulo))
            .ForMember(dest =>
                dest.Profesor,
                opt => opt.MapFrom(mf => $"{mf.MateriaProfesor.Profesor.ProfesorNavigation.Nombres} {mf.MateriaProfesor.Profesor.ProfesorNavigation.Apellidos}"));
}