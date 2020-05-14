using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Extensions
{
    public static class ControllerExtension
    {
        public static UserInfo GetAuthUser(this Controller controller)
        {
            return (UserInfo)controller.HttpContext.Items["User"];
        }
    }
}
