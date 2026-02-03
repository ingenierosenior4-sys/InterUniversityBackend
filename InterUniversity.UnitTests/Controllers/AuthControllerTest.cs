using InterUniversity.Api.Controllers;
using MediatR;
using Moq;

namespace InterUniversity.UnitTests.Controllers;

[TestFixture]
public class AuthControllerTest
{
    private Mock<IMediator> _mediatorMock = null!;
    private AuthController _controller = null!;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new AuthController(_mediatorMock.Object);
    }

    [Test]
    public async Task Login_DeberiaRetornarRespuesta_CuandoCredencialesValidas()
    {
        // Arrange
        var usuario = new UniversityApi.Features.Auth.Commands.Login.UsuarioLogin(1, "123", "Juan");
        var respuesta = new UniversityApi.Features.Auth.Commands.Login.LoginCommandResponse(usuario, "access-token", "refresh-token");

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<UniversityApi.Features.Auth.Commands.Login.LoginCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(respuesta);

        // Act
        var result = await _controller.Login(new UniversityApi.Features.Auth.Commands.Login.LoginCommand("123", "pwd"));

        // Assert
        Assert.That(result.AccessToken, Is.EqualTo("access-token"));
        _mediatorMock.Verify(m => m.Send(It.IsAny<UniversityApi.Features.Auth.Commands.Login.LoginCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public void Login_DeberiaLanzarExcepcion_CuandoMediatorFalla()
    {
        // Arrange
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<UniversityApi.Features.Auth.Commands.Login.LoginCommand>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("mediator-failure"));

        // Act & Assert
        Assert.ThrowsAsync<InvalidOperationException>(async () => await _controller.Login(new UniversityApi.Features.Auth.Commands.Login.LoginCommand("123", "pwd")));
        _mediatorMock.Verify(m => m.Send(It.IsAny<UniversityApi.Features.Auth.Commands.Login.LoginCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task Register_DeberiaRetornarRespuesta_CuandoRegistroValido()
    {
        // Arrange
        var usuario = new UniversityApi.Features.Auth.Commands.Login.UsuarioLogin(2, "456", "Maria");
        var respuesta = new UniversityApi.Features.Auth.Commands.Login.LoginCommandResponse(usuario, "access-2", "refresh-2");

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<UniversityApi.Features.Auth.Commands.Register.RegisterCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(respuesta);

        var comando = new UniversityApi.Features.Auth.Commands.Register.RegisterCommand("456", "Maria", "Perez", DateOnly.FromDateTime(DateTime.Now.AddYears(-20)), "pwd");

        // Act
        var result = await _controller.Register(comando);

        // Assert
        Assert.That(result.user.Nombre, Is.EqualTo("Maria"));
        _mediatorMock.Verify(m => m.Send(It.IsAny<UniversityApi.Features.Auth.Commands.Register.RegisterCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public void Register_DeberiaLanzarExcepcion_CuandoMediatorFalla()
    {
        // Arrange
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<UniversityApi.Features.Auth.Commands.Register.RegisterCommand>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("mediator-failure"));

        var comando = new UniversityApi.Features.Auth.Commands.Register.RegisterCommand("456", "Maria", "Perez", DateOnly.FromDateTime(DateTime.Now.AddYears(-20)), "pwd");

        // Act & Assert
        Assert.ThrowsAsync<InvalidOperationException>(async () => await _controller.Register(comando));
        _mediatorMock.Verify(m => m.Send(It.IsAny<UniversityApi.Features.Auth.Commands.Register.RegisterCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }

}
