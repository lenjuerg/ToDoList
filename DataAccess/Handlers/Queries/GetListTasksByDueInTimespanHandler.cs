using Application.Dto.Query;
using Application.Interfaces;
using Application.Queries;
using DataAccess.EfcCode;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Handlers.Queries
{
    public class GetListTasksByDueInTimeSpanHandler : IRequestHandler<GetListTasksByDueInTimeSpanQuery, List<GetListTaskDto>>
    {
        private readonly DataContext _context;
        private readonly IUserIdentityService _identity;
        public GetListTasksByDueInTimeSpanHandler(DataContext context, IUserIdentityService identity)
        {
            _context = context;
            _identity = identity;
        }

        public async Task<List<GetListTaskDto>> Handle(GetListTasksByDueInTimeSpanQuery request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            var userId = _identity.GetUserId();

            var currentDateTime = DateTime.UtcNow;

            var tasks = await _context.ListTasks
                .AsNoTracking()
                .Include(t => t.ToDoList)
                .Where(t => t.ToDoList.UserId == userId && EF.Functions.DateDiffHour(currentDateTime, t.DueDateTime) <= request.dueInHours)
                .Select(t => new GetListTaskDto
                {
                    Id = t.Id,
                    Description = t.Description,
                    DueDate = DateOnly.FromDateTime(t.DueDateTime),
                    DueTime = TimeOnly.FromDateTime(t.DueDateTime),
                    Finished = t.Finished,
                })
                .ToListAsync();

            return tasks ?? new List<GetListTaskDto>();
        }
    }
}
