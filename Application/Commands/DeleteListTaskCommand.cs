using MediatR;

namespace Application.Commands
{
    public record DeleteListTaskCommand(int id) : IRequest;
}
