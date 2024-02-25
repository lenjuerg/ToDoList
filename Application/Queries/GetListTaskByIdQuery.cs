using Application.Dto.Query;
using MediatR;

namespace Application.Queries
{
    public record GetListTaskByIdQuery(int id) : IRequest<GetListTaskDto>;
}
