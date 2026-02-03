using InterUniversity.Api.Controllers;
using InterUniversity.Application.Features.Estudiantes.Commands.Create;
using InterUniversity.Application.Features.Estudiantes.Commands.Delete;
using InterUniversity.Application.Features.Estudiantes.Commands.Update;
using InterUniversity.Application.Features.Estudiantes.Queries.Get;
using InterUniversity.Application.Features.Estudiantes.Queries.GetPrograma;
using InterUniversity.Domain.Dtos;
using MediatR;
using Moq;
using UniversityApi.Features.Estudiantes.Queries.Get;
using UniversityApi.Features.Estudiantes.Queries.GetEstudiante;
using UniversityApi.Features.Estudiantes.Queries.GetPrograma;

namespace InterUniversity.UnitTests.Controllers;

[TestFixture]
public class EstudianteControllerTest
{
    private Mock<IMediator> _mediatorMock = null!;
    private EstudianteController _controller = null!;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new EstudianteController(_mediatorMock.Object);
    }

    [Test]
    public async Task CreateEstudiante_DeberiaEjecutarse_CuandoValido()
    {
        var comando = new CreateEstudianteCommand();

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<CreateEstudianteCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        await _controller.CreateEstudiante(comando);

        _mediatorMock.Verify(m => m.Send(It.IsAny<CreateEstudianteCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public void CreateEstudiante_DeberiaLanzarExcepcion_CuandoMediatorFalla()
    {
        var comando = new CreateEstudianteCommand();

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<CreateEstudianteCommand>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("mediator-failure"));

        Assert.ThrowsAsync<InvalidOperationException>(async () => await _controller.CreateEstudiante(comando));
        _mediatorMock.Verify(m => m.Send(It.IsAny<CreateEstudianteCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task UpdateEstudiante_DeberiaEjecutarse_CuandoValido()
    {
        var comando = new UpdateEstudianteCommand(1, "123", "Juan", "Perez", DateOnly.FromDateTime(DateTime.Now.AddYears(-20)));

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<UpdateEstudianteCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        await _controller.UpdateEstudiante(comando);

        _mediatorMock.Verify(m => m.Send(It.IsAny<UpdateEstudianteCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public void UpdateEstudiante_DeberiaLanzarExcepcion_CuandoMediatorFalla()
    {
        var comando = new UpdateEstudianteCommand(1, "123", "Juan", "Perez", DateOnly.FromDateTime(DateTime.Now.AddYears(-20)));

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<UpdateEstudianteCommand>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("mediator-failure"));

        Assert.ThrowsAsync<InvalidOperationException>(async () => await _controller.UpdateEstudiante(comando));
        _mediatorMock.Verify(m => m.Send(It.IsAny<UpdateEstudianteCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task DeleteEstudiante_DeberiaEjecutarse_CuandoValido()
    {
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<DeleteEstudianteCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        await _controller.DeleteEstudiante(1);

        _mediatorMock.Verify(m => m.Send(It.IsAny<DeleteEstudianteCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public void DeleteEstudiante_DeberiaLanzarExcepcion_CuandoMediatorFalla()
    {
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<DeleteEstudianteCommand>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("mediator-failure"));

        Assert.ThrowsAsync<InvalidOperationException>(async () => await _controller.DeleteEstudiante(1));
        _mediatorMock.Verify(m => m.Send(It.IsAny<DeleteEstudianteCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task Get_DeberiaRetornarPaginado_CuandoHayEstudiantes()
    {
        var paged = new PagedResult<GetEstudiantesQueryResponse>
        {
            Results = new[] { new GetEstudiantesQueryResponse { NumeroIdentificacion = "123", Nombres = "Ana", Apellidos = "Lopez", FechaInscrito = DateTime.UtcNow } },
            RowsCount = 1,
            PageCount = 1,
            PageSize = 10,
            CurrentPage = 1
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetEstudiantesQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(paged);

        var query = new GetEntityQuery { PageSize = 10, CurrentPage = 1 };

        var result = await _controller.Get(query);

        Assert.That(result.Results.First().Nombres, Is.EqualTo("Ana"));
        _mediatorMock.Verify(m => m.Send(It.IsAny<GetEstudiantesQuery>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public void Get_DeberiaLanzarExcepcion_CuandoMediatorFalla()
    {
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetEstudiantesQuery>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("mediator-failure"));

        var query = new GetEntityQuery { PageSize = 10, CurrentPage = 1 };

        Assert.ThrowsAsync<InvalidOperationException>(async () => await _controller.Get(query));
        _mediatorMock.Verify(m => m.Send(It.IsAny<GetEstudiantesQuery>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task GetEstudiante_DeberiaRetornar_CuandoExiste()
    {
        var respuesta = new GetEstudianteQueryResponse
        {
            EstudianteId = 1,
            NumeroIdentificacion = "123",
            Nombres = "Luis",
            Apellidos = "Gomez",
            Creditos = 20,
            FechaNacimiento = DateOnly.FromDateTime(DateTime.Now.AddYears(-20)),
            FechaInscrito = DateTime.UtcNow
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetEstudianteQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(respuesta);

        var result = await _controller.GetEstudiante(1);

        Assert.That(result.Nombres, Is.EqualTo("Luis"));
        _mediatorMock.Verify(m => m.Send(It.IsAny<GetEstudianteQuery>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public void GetEstudiante_DeberiaLanzarExcepcion_CuandoMediatorFalla()
    {
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetEstudianteQuery>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("mediator-failure"));

        Assert.ThrowsAsync<InvalidOperationException>(async () => await _controller.GetEstudiante(1));
        _mediatorMock.Verify(m => m.Send(It.IsAny<GetEstudianteQuery>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task GetPrograma_DeberiaRetornar_CuandoExiste()
    {
        var respuesta = new GetProgramaQueryResponse("Ingenieria", "2026-1", 160);

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetProgramaQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(respuesta);

        var result = await _controller.GetPrograma();

        Assert.That(result.Especializacion, Is.EqualTo("Ingenieria"));
        _mediatorMock.Verify(m => m.Send(It.IsAny<GetProgramaQuery>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public void GetPrograma_DeberiaLanzarExcepcion_CuandoMediatorFalla()
    {
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetProgramaQuery>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("mediator-failure"));

        Assert.ThrowsAsync<InvalidOperationException>(async () => await _controller.GetPrograma());
        _mediatorMock.Verify(m => m.Send(It.IsAny<GetProgramaQuery>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
