using Application.Dto.Request;
using MediatR;

namespace Application.Commands
{
    public record CreateNewToDoListCommand(CreateNewToDoListDto newList) : IRequest;
}
