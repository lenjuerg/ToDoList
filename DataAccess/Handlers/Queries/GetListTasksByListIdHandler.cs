using Application.Dto.Query;
using Application.Interfaces;
using Application.Queries;
using DataAccess.EfcCode;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Handlers.Queries
{
    public class GetListTasksByListIdHandler : IRequestHandler<GetListTasksByListIdQuery, List<GetListTaskDto>>
    {
        private readonly DataContext _context;
        private readonly IUserIdentityService _identity;
        public GetListTasksByListIdHandler(DataContext context, IUserIdentityService identity)
        {
            _context = context;
            _identity = identity;
        }

        public async Task<List<GetListTaskDto>> Handle(GetListTasksByListIdQuery request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            var userId = _identity.GetUserId();

            var tasks = _context.ListTasks
                .AsNoTracking()
                .Include(t => t.ToDoList)
                .Where(t => t.ToDoListId == request.id && t.ToDoList.UserId == userId)
                .Select(t => new GetListTaskDto
                {
                    Id = t.Id,
                    Description = t.Description,
                    DueDate = DateOnly.FromDateTime(t.DueDateTime),
                    DueTime = TimeOnly.FromDateTime(t.DueDateTime),
                    Finished = t.Finished,
                })
                .ToListAsync();

            return await tasks ?? new List<GetListTaskDto>();
        }
    }
}
