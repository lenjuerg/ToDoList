using Application.Commands;
using Application.Dto.Query;
using Application.Interfaces;
using DataAccess.EfcCode;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Handlers.Queries
{
    public class GetToDoListsByUserIdHandler : IRequestHandler<GetToDoListsByUserIdQuery, List<GetToDoListDto>>
    {
        private readonly DataContext _context;
        private readonly IUserIdentityService _identity;

        public GetToDoListsByUserIdHandler(DataContext context, IUserIdentityService identity)
        {
            _context = context;
            _identity = identity;
        }

        public async Task<List<GetToDoListDto>> Handle(GetToDoListsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var userId = _identity.GetUserId();

            return await _context.ToDoLists
                    .AsNoTracking()
                    .Where(l => l.UserId == userId)
                    .Select(l => new GetToDoListDto
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
