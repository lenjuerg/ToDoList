using Application.Dto.Query;
using Application.Interfaces;
using Application.Queries;
using DataAccess.EfcCode;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DataAccess.Handlers.Queries
{
    public class GetToDoListByIdHandler : IRequestHandler<GetToDoListByIdQuery, GetToDoListByIdDto>
    {
        private readonly DataContext _context;
        public GetToDoListByIdHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<GetToDoListByIdDto> Handle(GetToDoListByIdQuery request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            if (request.id == 0)
                throw new ArgumentException("Id is empty.");

            var list = await _context.ToDoLists
                .AsNoTracking()
                .Include(l => l.Tasks)
                .Select(l => new GetToDoListByIdDto
                {
                    Id = l.Id,
                    Name = l.Name,
                    Description = l.Description,
                    Status = l.Status,
                    Tasks = l.Tasks.Select(t => new GetListTaskDto
                    {
                        Id = t.Id,
                        Description = t.Description,
                        DueDate = DateOnly.FromDateTime(t.DueDateTime),
                        DueTime = TimeOnly.FromDateTime(t.DueDateTime),
                        Finished = t.Finished,
                    }).ToList()
                })
                .Where(l => l.Id == request.id)
                .FirstOrDefaultAsync();
                
            if (list is null)
                throw new Exception("List not found.");

            return list;
        }
    }
}
