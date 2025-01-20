using System.Security.Claims;

namespace Qyrenx.CustomMidlleware
{
    public class IdAcessMiddleware
    {
            private readonly RequestDelegate _next;
            private object context;

            public IdAcessMiddleware(RequestDelegate next)
            {
                _next = next;
            }

            public async Task InvokeAsync(HttpContext context)
            {
                if (context.User.Identity?.IsAuthenticated == true)
                {
                    var idClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);
                    if (idClaim != null)
                    {

                        context.Items["Id"] = idClaim.Value;
                    }
                }
                await _next(context);
            }
        }
}
