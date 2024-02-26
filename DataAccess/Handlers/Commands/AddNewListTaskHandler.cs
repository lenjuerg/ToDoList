using Application.Commands;
using DataAccess.EfcCode;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using ToDoListApi.ExceptionHandling.Exceptions;

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
            var list = await _context.ToDoLists
                .AsNoTracking()
                .SingleOrDefaultAsync(l => l.Id == request.newTask.ToDoListId);

            if (list is null)
                throw new EntityNotFoundException("List not found.");

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
