using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManagerBackend.Models;

namespace ManagerBackend.Extensions
{
    public static class ControllerExtension
    {
        public static UserInfo GetAuthUser(this Controller controller)
        {
            return (UserInfo)controller.HttpContext.Items["User"];
        }
    }
}
