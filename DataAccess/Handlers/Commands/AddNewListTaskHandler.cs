using Application.Commands;
using Application.Interfaces;
using DataAccess.EfcCode;
using MediatR;
using Models;

namespace DataAccess.Handlers.Commands
{
    public class AddNewListTaskHandler : IRequestHandler<AddNewListTaskCommand>
    {
        private readonly DataContext _context;
        private readonly IUserIdentityService _identity;

        public AddNewListTaskHandler(DataContext context, IUserIdentityService identity)
        {
            _context = context;
            _identity = identity;
        }

        public async Task Handle(AddNewListTaskCommand request, CancellationToken cancellationToken)
        {
            var list = await _context.ToDoLists.FindAsync(request.newTask.ToDoListId);

            if (list is null)
                throw new Exception("List not found.");

            if (list.UserId != _identity.GetUserId())
                throw new UnauthorizedAccessException();

            var newTask = new ListTask
            {
                ToDoListId = request.newTask.ToDoListId,
                Description = request.newTask.Description,
                DueDateTime = new DateTime(request.newTask.DueDate, request.newTask.DueTime, DateTimeKind.Utc),
                Finished = request.newTask.Finished,
            };

            _context.ListTasks.Add(newTask);
            _context.SaveChanges();
        }
    }
}
