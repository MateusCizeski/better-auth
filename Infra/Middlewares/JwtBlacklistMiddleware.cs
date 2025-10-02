using Domain.BlacklistedTokens;
using Microsoft.AspNetCore.Http;

namespace Infra.Middlewares
{
    public class JwtBlacklistMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtBlacklistMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        //public async Task Invoke(HttpContext context, IRepBlacklistedToken repBlacklistedToken)
        //{
        //    var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        //    if (!string.IsNullOrEmpty(token))
        //    {
        //        var blacklisted = repBlacklistedToken.Get().Any(b => b.Token == token);
        //        if (blacklisted)
        //        {
        //            context.Response.StatusCode = 401;
        //            await context.Response.WriteAsync("Token revoked.");
        //            return;
        //        }
        //    }

        //    await _next(context);
        //}
    }
}
