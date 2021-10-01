using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CourseProject.Middleware
{
    public class CookieMiddleware
    {
        
        // TODO: remove CookieMiddleware?
        private readonly RequestDelegate _next;

        public CookieMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                string jwtCookie = context.Request.Cookies["jwt"];
                if (!string.IsNullOrWhiteSpace(jwtCookie))
                {
                    
                }
            }
            catch
            {
                Console.WriteLine("NO COOKIES");
            }

            await _next(context);
        }
    }
}