using Application.Commands;
using Application.Interfaces;
using DataAccess.EfcCode;
using MediatR;
using Models;

namespace DataAccess.Handlers.Commands
{
    public class CreateNewToDoListHandler : IRequestHandler<CreateNewToDoListCommand>
    {
        private readonly DataContext _context;
        private readonly IUserIdentityService _identity;

        public CreateNewToDoListHandler(DataContext context, IUserIdentityService identity)
        {
            _context = context;
            _identity = identity;
        }

        public async Task Handle(CreateNewToDoListCommand request, CancellationToken cancellationToken)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            if (request.newList.Name == string.Empty) throw new ArgumentException(nameof(request.newList));

            var newList = new ToDoList
            {
                UserId = _identity.GetUserId(),
                Name = request.newList.Name,
                Description = request.newList.Description,
                Status = Status.Open
            };

            await _context.AddAsync(newList);
            await _context.SaveChangesAsync();
        }
    }
}
