using InterUniversity.Domain.Entities;
using InterUniversity.Domain.Repositories;

namespace InterUniversity.Infrastructure.Repositories;

public sealed class ProfesorRepository(UniversidadDbContext dbContext) : Repository<Profesor>(dbContext), IProfesorRepository;
