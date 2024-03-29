﻿using Application.Commands;
using DataAccess.EfcCode;
using MediatR;
using ToDoListApi.ExceptionHandling.Exceptions;

namespace DataAccess.Handlers.Commands
{
    public class UpdateListTaskHandler : IRequestHandler<UpdateListTaskCommand>
    {
        private readonly DataContext _context;

        public UpdateListTaskHandler(DataContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateListTaskCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            var task = await _context.ListTasks.FindAsync(request.update.Id);

            if (task is null)
                throw new EntityNotFoundException("Task not found.");

            task.Description = request.update.Description;
            task.DueDateTime = request.update.DueDateTime.UtcDateTime;
            task.Finished = request.update.Finished;
            await _context.SaveChangesAsync();
        }
    }
}
