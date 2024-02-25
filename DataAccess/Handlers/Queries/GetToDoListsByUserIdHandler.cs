using Application.Commands;
using Application.Dto.Query;
using Application.Interfaces;
using DataAccess.EfcCode;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Handlers.Queries
{
    public class GetToDoListsByUserIdHandler : IRequestHandler<GetToDoListsByUserIdQuery, List<GetToDoListsByUserIdDto>>
    {
        private readonly DataContext _context;

        public GetToDoListsByUserIdHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<List<GetToDoListsByUserIdDto>> Handle(GetToDoListsByUserIdQuery request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            return await _context.ToDoLists
                    .AsNoTracking()
                    .Select(l => new GetToDoListsByUserIdDto
                    {
                        Id = l.Id,
                        Name = l.Name,
                        Description = l.Description,
                        Status = l.Status
                    })
                    .ToListAsync();
        }
    }
}
