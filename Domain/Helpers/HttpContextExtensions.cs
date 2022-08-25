using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Domain.Helpers
{
    public static class HttpContextExtensions
    {
        public static string GetCurrentUserId(this IHttpContextAccessor contextAccessor)
        {
            if (contextAccessor == null)
                throw new ArgumentNullException(nameof(contextAccessor));

            if (!contextAccessor.HttpContext.User.Identity.IsAuthenticated)
                return string.Empty;

            return contextAccessor.HttpContext.User.GetCurrentUserId();
        }

        public static string GetCurrentUserId(this ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal == null)
                throw new ArgumentNullException(nameof(claimsPrincipal));

            var claim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid);
            if (claim == null)
                return string.Empty;

            return claim.Value;
        }

        public static string GetCurrentUserrole(this IHttpContextAccessor contextAccessor)
        {
            if (contextAccessor == null)
                throw new ArgumentNullException(nameof(contextAccessor));

            if (!contextAccessor.HttpContext.User.Identity.IsAuthenticated)
                return string.Empty;

            return contextAccessor.HttpContext.User.GetCurrentUserrole();
        }

        public static string GetCurrentUserrole(this ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal == null)
                throw new ArgumentNullException(nameof(claimsPrincipal));

            var claim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (claim == null)
                throw new ArgumentNullException(nameof(claim));

            return claim.Value;
        }
    }
}
