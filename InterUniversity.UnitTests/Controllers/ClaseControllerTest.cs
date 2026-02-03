using InterUniversity.Api.Controllers;
using InterUniversity.Application.Features.Clases.Commands.Create;
using InterUniversity.Application.Features.Clases.Queries.Get;
using InterUniversity.Application.Features.Clases.Queries.GetClase;
using InterUniversity.Domain.Dtos;
using MediatR;
using Moq;

namespace InterUniversity.UnitTests.Controllers;

[TestFixture]
public class ClaseControllerTest
{
    private Mock<IMediator> _mediatorMock = null!;
    private ClaseController _controller = null!;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new ClaseController(_mediatorMock.Object);
    }

    [Test]
    public async Task Post_DeberiaEjecutarse_CuandoClasesValidas()
    {
        // Arrange
        var clases = new[] { new ClaseDto(1, 2) };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<CreateClaseCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await _controller.Post(clases);

        // Assert
        _mediatorMock.Verify(m => m.Send(It.IsAny<CreateClaseCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public void Post_DeberiaLanzarExcepcion_CuandoMediatorFalla()
    {
        // Arrange
        var clases = new[] { new InterUniversity.Domain.Dtos.ClaseDto(1, 2) };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<CreateClaseCommand>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("mediator-failure"));

        // Act & Assert
        Assert.ThrowsAsync<InvalidOperationException>(async () => await _controller.Post(clases));
        _mediatorMock.Verify(m => m.Send(It.IsAny<CreateClaseCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task Get_DeberiaRetornarLista_CuandoHayClases()
    {
        // Arrange
        var respuesta = new[]
        {
            new GetClasesQueryResponse { MateriaId = 1, ProfesorId = 2, Titulo = "Matematica", Profesor = "Dr. X" }
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetClasesQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((IEnumerable<GetClasesQueryResponse>)respuesta);

        // Act
        var result = await _controller.Get(1);

        // Assert
        Assert.That(result.First().Titulo, Is.EqualTo("Matematica"));
        _mediatorMock.Verify(m => m.Send(It.IsAny<GetClasesQuery>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public void Get_DeberiaLanzarExcepcion_CuandoMediatorFalla()
    {
        // Arrange
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetClasesQuery>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("mediator-failure"));

        // Act & Assert
        Assert.ThrowsAsync<InvalidOperationException>(async () => await _controller.Get(1));
        _mediatorMock.Verify(m => m.Send(It.IsAny<GetClasesQuery>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task GetClase_DeberiaRetornar_CuandoExiste()
    {
        // Arrange
        var respuesta = new GetClaseQueryResponse
        {
            MateriaId = 3,
            ProfesorId = 4,
            Titulo = "Fisica",
            Profesor = "Dra. Y",
            Estudiantes = new[] { "Est1", "Est2" }
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetClaseQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(respuesta);

        // Act
        var result = await _controller.GetClase(4, 3);

        // Assert
        Assert.That(result.Titulo, Is.EqualTo("Fisica"));
        Assert.That(result.Estudiantes.Length, Is.EqualTo(2));
        _mediatorMock.Verify(m => m.Send(It.IsAny<GetClaseQuery>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public void GetClase_DeberiaLanzarExcepcion_CuandoMediatorFalla()
    {
        // Arrange
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetClaseQuery>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("mediator-failure"));

        // Act & Assert
        Assert.ThrowsAsync<InvalidOperationException>(async () => await _controller.GetClase(4, 3));
        _mediatorMock.Verify(m => m.Send(It.IsAny<GetClaseQuery>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
