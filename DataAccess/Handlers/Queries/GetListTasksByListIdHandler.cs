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
        public GetListTasksByListIdHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<List<GetListTaskDto>> Handle(GetListTasksByListIdQuery request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            var tasks = _context.ListTasks
                .AsNoTracking()
                .Where(t => t.ToDoListId == request.id)
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
