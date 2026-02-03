using InterUniversity.Application.Features.Estudiantes.Commands.Create;
using InterUniversity.Application.Features.Estudiantes.Commands.Delete;
using InterUniversity.Application.Features.Estudiantes.Commands.Update;
using InterUniversity.Application.Features.Estudiantes.Queries.Get;
using InterUniversity.Application.Features.Estudiantes.Queries.GetPrograma;
using InterUniversity.Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityApi.Features.Estudiantes.Queries.Get;
using UniversityApi.Features.Estudiantes.Queries.GetEstudiante;
using UniversityApi.Features.Estudiantes.Queries.GetPrograma;

namespace InterUniversity.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class EstudianteController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public Task CreateEstudiante(CreateEstudianteCommand command)
        => mediator.Send(command);

    [HttpPut]
    public Task UpdateEstudiante(UpdateEstudianteCommand command)
        => mediator.Send(command);

    [HttpDelete("{id}")]
    public Task DeleteEstudiante(int id)
        => mediator.Send(new DeleteEstudianteCommand(id));

    [HttpGet]
    public Task<PagedResult<GetEstudiantesQueryResponse>> Get([FromQuery] GetEntityQuery query)
       => mediator.Send(new GetEstudiantesQuery(query));

    [HttpGet("{id}")]
    public Task<GetEstudianteQueryResponse> GetEstudiante(int id)
       => mediator.Send(new GetEstudianteQuery(id));


    [HttpGet("[action]")]
    public Task<GetProgramaQueryResponse> GetPrograma()
        => mediator.Send(new GetProgramaQuery());
}
