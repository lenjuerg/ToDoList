using Application.Dto.Commands;
using MediatR;

namespace Application.Commands
{
    public record UpdateToDoListCommand(UpdateToDoListDto updatedList) : IRequest;
}
