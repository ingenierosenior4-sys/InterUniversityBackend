using InterUniversity.Domain.Dtos;
using MediatR;

namespace InterUniversity.Application.Features.Clases.Commands.Create;

public record struct CreateClaseCommand(IEnumerable<ClaseDto> Clases) : IRequest;