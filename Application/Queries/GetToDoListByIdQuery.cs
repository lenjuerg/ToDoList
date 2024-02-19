using Application.Dto.Query;
using MediatR;

namespace Application.Queries
{
    public record GetToDoListByIdQuery(int id) : IRequest<GetToDoListDto>;
}
