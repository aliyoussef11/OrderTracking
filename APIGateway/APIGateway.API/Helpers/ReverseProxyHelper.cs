using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Yarp.ReverseProxy.Transforms;

namespace APIGateway.API.Helpers
{
    public static class ReverseProxyHelper
    {
        private static class ProxyHeaders
        {
            public const string EmployeeId = "Header-EmployeeId";
        }

        // Adds custom headers related to the authenticated user to the outgoing proxy request
        public static async ValueTask AddUserHeadersAsync(RequestTransformContext transformContext)
        {
            var httpContext = transformContext.HttpContext;

            if (httpContext.User.Identity?.IsAuthenticated != true)
            {
                return;
            }

            // Get user ID from 'sub' claim or fallback to first claim value
            var userId = httpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value
                         ?? httpContext.User.Claims.FirstOrDefault()?.Value;

            if (!string.IsNullOrEmpty(userId))
            {
                AddOrUpdateHeader(transformContext.ProxyRequest.Headers, ProxyHeaders.EmployeeId, userId);
            }

            await Task.CompletedTask;
        }

        public static void AddOrUpdateHeader(HttpRequestHeaders headers, string headerName, string headerValue)
        {
            if (headers.Contains(headerName))
            {
                headers.Remove(headerName);
            }
            headers.Add(headerName, headerValue);
        }

        // True if the path is other than authentication; otherwise, false
        public static bool IsAuthenticationExemptPath(PathString requestPath)
        {
            return requestPath.StartsWithSegments("/api/auth");
        }

        // True if the user is authenticated; otherwise, false.
        public static bool IsUserAuthenticated(HttpContext context)
        {
            return context.User?.Identity != null && context.User.Identity.IsAuthenticated;
        }
    }
}
