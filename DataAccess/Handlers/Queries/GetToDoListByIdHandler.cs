using Application.Dto.Query;
using Application.Interfaces;
using Application.Queries;
using DataAccess.EfcCode;
using MediatR;

namespace DataAccess.Handlers.Queries
{
    public class GetToDoListByIdHandler : IRequestHandler<GetToDoListByIdQuery, GetToDoListDto>
    {
        private readonly DataContext _context;
        private readonly IUserIdentityService _identity;
        public GetToDoListByIdHandler(DataContext context, IUserIdentityService identity)
        {
            _context = context;
            _identity = identity;
        }

        public async Task<GetToDoListDto> Handle(GetToDoListByIdQuery request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            if (request.id == 0)
                throw new ArgumentException("Id is empty.");

            var userId = _identity.GetUserId();

            var list = await _context.ToDoLists.FindAsync(request.id);

            if (list is null)
                throw new Exception("List not found.");

            if (list.UserId != userId)
                throw new UnauthorizedAccessException("List doesn't belong to user.");

            return new GetToDoListDto
            {
                Id = list.Id,
                Name = list.Name,
                Description = list.Description,
                Status = list.Status
            };
        }
    }
}
