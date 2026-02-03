using InterUniversity.Domain.Entities;
using InterUniversity.Domain.Repositories;

namespace InterUniversity.Infrastructure.Repositories;

public sealed class CreditoRepository(UniversidadDbContext dbContext) : Repository<Credito>(dbContext), ICreditoRepository;
