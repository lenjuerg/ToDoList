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

        public AddNewListTaskHandler(DataContext context)
        {
            _context = context;
        }

        public async Task Handle(AddNewListTaskCommand request, CancellationToken cancellationToken)
        {
            var list = await _context.ToDoLists.FindAsync(request.newTask.ToDoListId);

            if (list is null)
                throw new Exception("List not found.");

            var newTask = new ListTask
            {
                ToDoListId = request.newTask.ToDoListId,
                Description = request.newTask.Description,
                DueDateTime = request.newTask.DueDateTime.UtcDateTime,
                Finished = request.newTask.Finished,
            };

            _context.ListTasks.Add(newTask);
            _context.SaveChanges();
        }
    }
}
