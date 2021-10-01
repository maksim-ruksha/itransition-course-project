using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CourseProject.Auth;
using CourseProject.BLL.Models;
using CourseProject.BLL.Services;
using CourseProject.DAL.EF;
using CourseProject.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CourseProject.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceProvider _serviceProvider;

        public JwtMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
        {
            _next = next;
            _serviceProvider = serviceProvider;
        }

        public async Task Invoke(HttpContext context)
        {
            
            
            try
            {
                // TODO: move key to config
                string token = context.Request.Cookies["jwt"];
                if (!string.IsNullOrWhiteSpace(token))
                    AttachUserToContext(context, token);
            }
            catch
            {
                // no cookies - no auth
            }

            await _next(context);
        }
        
        private void AttachUserToContext(HttpContext context, string token)
        {
            try
            {
                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    Service<UserModel, UserEntity> userService =
                        scope.ServiceProvider.GetRequiredService<Service<UserModel, UserEntity>>();
                    JwtSecurityToken jwtToken = JwtCoder.Decode(token);
                    Guid userId = Guid.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                    context.Items["User"] = userService.FindByIdAsync(userId);
                }
            }
            catch
            {
                // sus
            }
        }
    }
}