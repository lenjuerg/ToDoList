using Application.Dto.Commands;
using MediatR;

namespace Application.Commands
{
    public record UpdateListTaskCommand(UpdateListTaskDto update) : IRequest;
}
