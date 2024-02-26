using Application.Commands;
using DataAccess.EfcCode;
using MediatR;
using ToDoListApi.ExceptionHandling.Exceptions;

namespace DataAccess.Handlers.Commands
{
    public class DeleteToDoListHandler : IRequestHandler<DeleteToDoListCommand>
    {
        private readonly DataContext _context;

        public DeleteToDoListHandler(DataContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteToDoListCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            var list = await _context.ToDoLists.FindAsync(request.listId);

            if (list is null)
                throw new EntityNotFoundException("List not found.");

            _context.ToDoLists.Remove(list);
            _context.SaveChanges();
        }
    }
}
