//using Qyrenx.Services.JwtServices;

//namespace Qyrenx.CustomMidlleware
//{
//    public class JwtMiddleware
//    {
//        private readonly IJwtService _jwtService;
//        private readonly RequestDelegate _next;
//        public JwtMiddleware(IJwtService jwtService, RequestDelegate next)
//        {  
//         _next = next;
//         _jwtService = jwtService;  
//        }

//        public async Task InvokeAsync(HttpContext context)
//        {
//            // Check if the request is for generating a token
//            if (context.Request.Path.StartsWithSegments("/generate-token") && context.Request.Method == "POST")
//            {
//                var Id = context.Request.Headers["Id"].ToString();
//                var Email = context.Request.Headers["Email"].ToString();
//                var role = context.Request.Headers["Role"].ToString(); // Role could be 'user', 'vendor', 'deliveryperson', 'admin'

//                if (string.IsNullOrEmpty(Id) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(role))
//                {
//                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
//                    await context.Response.WriteAsync("Missing required parameters.");
//                    return;
//                }

//                var token = _jwtService.GenerateJwt(Id, Email, role);

//                context.Response.StatusCode = StatusCodes.Status200OK;
//                await context.Response.WriteAsync(token);
//                return;
//            }

//            await _next(context);
//        }

//        }
//}
