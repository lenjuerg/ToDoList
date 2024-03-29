﻿using Application.Commands;
using DataAccess.EfcCode;
using MediatR;
using ToDoListApi.ExceptionHandling.Exceptions;

namespace DataAccess.Handlers.Commands
{
    public class DeleteListTaskHandler : IRequestHandler<DeleteListTaskCommand>
    {
        private readonly DataContext _context;

        public DeleteListTaskHandler(DataContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteListTaskCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            var task = await _context.ListTasks.FindAsync(request.id);

            if (task is null)
                throw new EntityNotFoundException("Task not found.");

            _context.ListTasks.Remove(task);
            await _context.SaveChangesAsync();
        }
    }
}
