using Application.Dto.Query;
using Application.Interfaces;
using Application.Queries;
using DataAccess.EfcCode;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Handlers.Queries
{
    public class GetListTaskById : IRequestHandler<GetListTaskByIdQuery, GetListTaskDto>
    {
        private readonly DataContext _context;
        private readonly IUserIdentityService _identity;
        public GetListTaskById(DataContext context, IUserIdentityService identity)
        {
            _context = context;
            _identity = identity;
        }

        public async Task<GetListTaskDto> Handle(GetListTaskByIdQuery request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            var userId = _identity.GetUserId();

            var task = await _context.ListTasks
                .AsNoTracking()
                .Include(t => t.ToDoList)
                .Where(t => t.Id == request.id && t.ToDoList.UserId == userId)
                .FirstOrDefaultAsync();

            if (task is null)
                throw new Exception("Task not found.");

            return new GetListTaskDto
            {
                Id = task.Id,
                Description = task.Description,
                DueDate = DateOnly.FromDateTime(task.DueDateTime),
                DueTime = TimeOnly.FromDateTime(task.DueDateTime),
                Finished = task.Finished,
            };
        }
    }
}
