using InterUniversity.Api.Controllers;
using InterUniversity.Application.Features.Materias.Queries.Get;
using MediatR;
using Moq;

namespace InterUniversity.UnitTests.Controllers;

[TestFixture]
public class MateriaControllerTest
{
    private Mock<IMediator> _mediatorMock = null!;
    private MateriaController _controller = null!;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new MateriaController(_mediatorMock.Object);
    }

    [Test]
    public async Task DeberiaRetornarMaterias_CuandoMediatorDevuelveLista()
    {
        // Arrange
        var expected = new List<GetMateriasQueryResponse>
        {
            new GetMateriasQueryResponse { MateriaId = 1, ProfesorId = 10, Titulo = "Matematicas", Creditos = 3, Profesor = "Profesor A" }
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetMateriasQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        // Act
        var result = await _controller.Get();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.EquivalentTo(expected));
        _mediatorMock.Verify(m => m.Send(It.IsAny<GetMateriasQuery>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public void DeberiaLanzarExcepcion_CuandoMediatorFalla()
    {
        // Arrange
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetMateriasQuery>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("mediator-failure"));

        // Act & Assert
        Assert.ThrowsAsync<InvalidOperationException>(async () => await _controller.Get());
        _mediatorMock.Verify(m => m.Send(It.IsAny<GetMateriasQuery>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
