using Application.Commands;
using Application.Interfaces;
using DataAccess.EfcCode;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Handlers.Commands
{
    public class UpdateListTaskHandler : IRequestHandler<UpdateListTaskCommand>
    {
        private readonly DataContext _context;
        private readonly IUserIdentityService _identity;

        public UpdateListTaskHandler(DataContext context, IUserIdentityService identity)
        {
            _context = context;
            _identity = identity;
        }

        public async Task Handle(UpdateListTaskCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            var userId = _identity.GetUserId();

            var task = await _context.ListTasks
                .Include(t => t.ToDoList)
                .Where(t => t.Id == request.update.Id && t.ToDoList.UserId == userId)
                .FirstOrDefaultAsync();

            if (task is null)
                throw new Exception("Task not found.");

            task.Description = request.update.Description;
            task.DueDateTime = request.update.DueDateTime.UtcDateTime;
            task.Finished = request.update.Finished;
            await _context.SaveChangesAsync();
        }
    }
}
