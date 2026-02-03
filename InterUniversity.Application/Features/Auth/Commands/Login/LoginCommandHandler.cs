using InterUniversity.Application.Abstractions.Encryption;
using InterUniversity.Application.Abstractions.Security;
using InterUniversity.Application.Exceptions;
using InterUniversity.Domain.Entities;
using InterUniversity.Domain.Repositories;
using MediatR;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UniversityApi.Features.Auth.Commands.Login;

namespace InterUniversity.Application.Features.Auth.Commands.Login;

public class LoginCommandHandler(
    IUsuarioRepository usuarioRepository,
    IJWTFactory jwtFactory) : IRequestHandler<LoginCommand, LoginCommandResponse>
{
    public async Task<LoginCommandResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var numero = request.NumeroIdentificacion.Trim();
        var user = await usuarioRepository.ObtenerUsuario(numero)
            ?? throw new ValidationException("Documento o contraseña incorrectos");

        if (!EncryptionPassword.CheckHash(request.Contrasena, user.Contrasena, user.Salt))
            throw new ValidationException("Documento o contraseña incorrectos");

        var sesionId = Guid.NewGuid().ToString();

        return new LoginCommandResponse(
            new UsuarioLogin(
                user.UsuarioId,
                user.NumeroIdentificacion,
                $"{user.Nombres} {user.Apellidos}"),
            CreateJWT(user, sesionId),
            sesionId);
    }

    private string CreateJWT(Usuario user, string sesionId)
    {
        var claimsIdentity = GetClaimsIdentity(user, sesionId);

        return jwtFactory.GenerateEncodedToken(claimsIdentity);
    }

    private static ClaimsIdentity GetClaimsIdentity(Usuario usuario, string sesionId)
    {
        var claimsIdentity = new ClaimsIdentity();
        claimsIdentity.AddClaims(new Claim[]
        {
            new(JwtRegisteredClaimNames.NameId, usuario.UsuarioId.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, usuario.UsuarioId.ToString()),
            new(JwtRegisteredClaimNames.Jti, sesionId)
        });

        return claimsIdentity;
    }
}
