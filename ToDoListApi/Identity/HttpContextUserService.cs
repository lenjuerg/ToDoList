using Application.Interfaces;
using System.Security.Claims;

namespace ToDoListApi.Identity
{
    public class HttpContextUserService : IUserIdentityService
    {
        private readonly IHttpContextAccessor _context;

        public HttpContextUserService(IHttpContextAccessor httpContext)
        {
            if (httpContext is null)
                throw new ArgumentNullException(nameof(httpContext));

            _context = httpContext;
        }

        public string GetUserId()
        {
            if (_context.HttpContext is null)
                throw new ArgumentNullException(nameof(_context.HttpContext));

            var userId = _context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId is null)
                throw new UnauthorizedAccessException();

            return userId;
        }
    }
}
