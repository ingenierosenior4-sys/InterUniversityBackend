using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniversityApi.Features.Auth.Commands.Login;
using UniversityApi.Features.Auth.Commands.Register;

namespace InterUniversity.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public Task<LoginCommandResponse> Login(LoginCommand command) =>
        mediator.Send(command);

    [HttpPost("register")]
    public Task<LoginCommandResponse> Register(RegisterCommand command) =>
        mediator.Send(command);
}
