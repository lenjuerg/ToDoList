using Application.Dto.Query;
using MediatR;

namespace Application.Queries
{
    public record GetListTasksByListIdQuery(int id) : IRequest<List<GetListTaskDto>>;
}
