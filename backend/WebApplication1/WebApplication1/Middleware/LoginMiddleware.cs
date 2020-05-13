using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using WebApplication1.Models.Services;
using WebApplication1.Services;

namespace WebApplication1.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class LoginMiddleware:IMiddleware
    {
        private LoginService loginService;
        private UserService loginedUsers;

        private static HashSet<PathString> whiteList = new HashSet<PathString>()
        {
            new PathString("/login")
        };
        public LoginMiddleware(LoginService loginService, UserService loginedUsers)
        {
            this.loginService = loginService;
            this.loginedUsers = loginedUsers;
        }
        
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context.Request.Path == new PathString("/login"))
            {
                await next?.Invoke(context);
                return;
            }
            if (whiteList.Contains(context.Request.Path))
            {
                await next?.Invoke(context);
                return;
            }

            if ((!context.Request.Headers.TryGetValue("Authorization", out var result)) ||
                (!long.TryParse(result.ToString(), out var token)) ||
               (!loginedUsers.ValidToken(token)))
            {
                context.Response.StatusCode = 401;
                return;
            }
            
            await next?.Invoke(context);
        }

    }

}
