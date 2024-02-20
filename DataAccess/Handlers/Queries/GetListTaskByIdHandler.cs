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

        public GetListTaskById(DataContext context)
        {
            _context = context;
        }

        public async Task<GetListTaskDto> Handle(GetListTaskByIdQuery request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            var task = await _context.ListTasks.FindAsync(request.id);

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
