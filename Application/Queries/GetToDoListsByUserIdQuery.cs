using Application.Dto.Query;
using MediatR;

namespace Application.Commands
{
    public record GetToDoListsByUserIdQuery() : IRequest<List<GetToDoListDto>>;
}
