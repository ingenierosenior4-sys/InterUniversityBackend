using InterUniversity.Application.Features.Clases.Commands.Create;
using InterUniversity.Application.Features.Clases.Queries.Get;
using InterUniversity.Application.Features.Clases.Queries.GetClase;
using InterUniversity.Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InterUniversity.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ClaseController(IMediator mediator) : ControllerBase
{

    [HttpPost]
    public Task Post(IEnumerable<ClaseDto> clases)
        => mediator.Send(new CreateClaseCommand(clases));

    [HttpGet("{id}")]
    public Task<IEnumerable<GetClasesQueryResponse>> Get(int id)
        => mediator.Send(new GetClasesQuery(id));

    [HttpGet("{profesorId}/{materiaId}")]
    public Task<GetClaseQueryResponse> GetClase(int profesorId, int materiaId)
        => mediator.Send(new GetClaseQuery(profesorId, materiaId));

}
