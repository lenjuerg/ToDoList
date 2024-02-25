using Application.Dto.Commands;
using MediatR;

namespace Application.Commands
{
    public record AddNewListTaskCommand(AddNewListTaskDto newTask) : IRequest;
}
