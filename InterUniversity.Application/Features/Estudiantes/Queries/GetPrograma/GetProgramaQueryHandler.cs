using InterUniversity.Application.Abstractions.Context;
using InterUniversity.Application.Exceptions;
using InterUniversity.Domain.Entities;
using InterUniversity.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Configuration;
using UniversityApi.Features.Estudiantes.Queries.GetPrograma;

namespace InterUniversity.Application.Features.Estudiantes.Queries.GetPrograma;

public class GetProgramaQueryHandler(
    IEstudianteRepository estudianteRepository,
    ICreditoRepository creditoRepository,
    IContextAccessor contextAccessor,
    IConfiguration configuration) : IRequestHandler<GetProgramaQuery, GetProgramaQueryResponse>
{

    public async Task<GetProgramaQueryResponse> Handle(GetProgramaQuery request, CancellationToken cancellationToken)
    {
        var userId = int.Parse(contextAccessor.UserId);
        if (await estudianteRepository.ExisteEstudiante(userId))
            throw new ValidationException("El estudiante ya esta inscrito");

        var idCredito = configuration.GetValue<int>("Parametros:CreditoIdEstudianteNuevo");

        var credito = await creditoRepository.FindAsync(idCredito) ?? new Credito();

        return new GetProgramaQueryResponse(
            "Ingeniera de sitemas",
            $"Primer semestre de pregrado {DateTime.Now.Year}",
            credito.Creditos);
    }
}