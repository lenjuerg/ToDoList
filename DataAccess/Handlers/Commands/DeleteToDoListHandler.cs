using Application.Commands;
using Application.Interfaces;
using DataAccess.EfcCode;
using MediatR;

namespace DataAccess.Handlers.Commands
{
    public class DeleteToDoListHandler : IRequestHandler<DeleteToDoListCommand>
    {
        private readonly DataContext _context;
        private readonly IUserIdentityService _identity;

        public DeleteToDoListHandler(DataContext context, IUserIdentityService identity)
        {
            _context = context;
            _identity = identity;
        }

        public async Task Handle(DeleteToDoListCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            var userId = _identity.GetUserId();

            var list = await _context.ToDoLists.FindAsync(request.listId);

            if (list is null)
                throw new ArgumentException("List not found.");

            if (list.UserId != userId)
                throw new UnauthorizedAccessException();

            _context.ToDoLists.Remove(list);
            _context.SaveChanges();
        }
    }
}
