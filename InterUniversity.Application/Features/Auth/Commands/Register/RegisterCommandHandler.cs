using InterUniversity.Application.Abstractions.Encryption;
using InterUniversity.Application.Exceptions;
using InterUniversity.Domain.Abstractions;
using InterUniversity.Domain.Entities;
using InterUniversity.Domain.Repositories;
using MediatR;
using UniversityApi.Features.Auth.Commands.Login;
using UniversityApi.Features.Auth.Commands.Register;

namespace InterUniversity.Application.Features.Auth.Commands.Register;

public class RegisterCommandHandler(
    IUsuarioRepository usuarioRepository,
    IUnitOfWork unitOfWork,
    IMediator mediator) : IRequestHandler<RegisterCommand, LoginCommandResponse>
{
    public async Task<LoginCommandResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var numero = request.NumeroIdentificacion.Trim();
        if (await usuarioRepository.ExisteUsuario(numero))
            throw new ValidationException("El numero de identificación ya esta registrado");

        var hash = EncryptionPassword.Hash(request.Contrasena);
        var user = new Usuario
        {
            NumeroIdentificacion = numero,
            Nombres = request.Nombres.Trim(),
            Apellidos = request.Apellidos.Trim(),
            FechaNacimiento = request.FechaNacimiento,
            Contrasena = hash.Password,
            Salt = hash.Salt
        };

        usuarioRepository.Add(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return await mediator.Send(new LoginCommand(user.NumeroIdentificacion, request.Contrasena), cancellationToken);
    }
}
