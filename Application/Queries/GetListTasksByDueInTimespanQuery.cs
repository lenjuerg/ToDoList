using Application.Dto.Query;
using MediatR;

namespace Application.Queries
{
    public record GetListTasksByDueInTimeSpanQuery(int dueInHours) : IRequest<List<GetListTaskDto>>;
}
