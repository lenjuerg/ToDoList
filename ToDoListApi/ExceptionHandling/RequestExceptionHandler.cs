using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ToDoListApi.ExceptionHandling.Exceptions;

namespace ToDoListApi.ExceptionHandling
{
    public class RequestExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            httpContext.Response.StatusCode = SetStatusCode(exception);
            await httpContext.Response.WriteAsJsonAsync(CreateProblemDetails(exception), cancellationToken);
            return true;
        }

        private int SetStatusCode(Exception exception) =>
            exception switch
            {
                EntityNotFoundException => StatusCodes.Status404NotFound,
                ArgumentException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError,
            };

        private ProblemDetails CreateProblemDetails(Exception exception)
        {
            return new ProblemDetails
            {
                Title = "An error occured.",
                Status = SetStatusCode(exception),
                Detail = exception.Message,
            };
        }
    }
}
