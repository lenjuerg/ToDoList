using Application.Commands;
using Application.Interfaces;
using DataAccess.EfcCode;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Handlers.Commands
{
    public class DeleteListTaskHandler : IRequestHandler<DeleteListTaskCommand>
    {
        private readonly DataContext _context;
        private readonly IUserIdentityService _identity;

        public DeleteListTaskHandler(DataContext context, IUserIdentityService identity)
        {
            _context = context;
            _identity = identity;
        }

        public async Task Handle(DeleteListTaskCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            var userId = _identity.GetUserId();

            var task = await _context.ListTasks
                .Include(t => t.ToDoList)
                .Where(t => t.Id == request.id && t.ToDoList.UserId == userId)
                .FirstOrDefaultAsync();

            if (task is null)
                throw new Exception("Task not found.");

            _context.ListTasks.Remove(task);
            await _context.SaveChangesAsync();
        }
    }
}
