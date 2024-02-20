using Application.Commands;
using Application.Interfaces;
using DataAccess.EfcCode;
using MediatR;

namespace DataAccess.Handlers.Commands
{
    public class UpdateToDoListHandler : IRequestHandler<UpdateToDoListCommand>
    {
        private readonly DataContext _context;

        public UpdateToDoListHandler(DataContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateToDoListCommand request, CancellationToken cancellationToken)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            if (request.updatedList.Name == string.Empty)
                throw new ArgumentException("Name is empty.");

            var list = await _context.ToDoLists.FindAsync(request.updatedList.Id);

            if (list is null)
                throw new ArgumentException("List not found.");

            list.Name = request.updatedList.Name;
            list.Description = request.updatedList.Description;
            list.Status = request.updatedList.Status;
            await _context.SaveChangesAsync();
        }
    }
}
