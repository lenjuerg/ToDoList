using MediatR;

namespace Application.Commands
{
    public record DeleteToDoListCommand(int listId) : IRequest;
}
