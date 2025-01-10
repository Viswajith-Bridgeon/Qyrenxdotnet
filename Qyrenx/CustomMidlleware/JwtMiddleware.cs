//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;

//public class JwtMiddleware
//{
//    private readonly RequestDelegate _next;

//    public JwtMiddleware(RequestDelegate next)
//    {
//        _next = next;
//    }

//    public async Task InvokeAsync(HttpContext context)
//    {
//        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

//        if (token != null)
//        {
//            try
//            {
//                var tokenHandler = new JwtSecurityTokenHandler();
//                var jwtToken = tokenHandler.ReadJwtToken(token);

//                var expiration = jwtToken.Claims.FirstOrDefault(c => c.Type == "exp")?.Value;

//                if (expiration != null && DateTime.UtcNow > DateTimeOffset.FromUnixTimeSeconds(long.Parse(expiration)).UtcDateTime)
//                {
//                    throw new SecurityTokenExpiredException("Token has expired.");
//                }
//            }
//            catch (Exception ex)
//            {
//                context.Response.StatusCode = 401;
//                await context.Response.WriteAsync("Unauthorized: " + ex.Message);
//                return;
//            }
//        }

//        await _next(context);
//    }
//}
