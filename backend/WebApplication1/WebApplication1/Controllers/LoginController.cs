using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Services;
using WebApplication1.Models;
using WebApplication1.Models.Services;

namespace WebApplication1.Controllers
{
    //登录时调用的函数类
    [Produces("application/json")]
    public class LoginController: Controller
    {

        private LoginService loginService;
        private UserService userService;

        public LoginController(LoginService loginService,UserService userService)
        {
            this.loginService = loginService;
        }
        //真正的调用的判断函数，返回的是json
        //https://localhost:5001/Login/?username=1&password=1
        [HttpGet]
        public IActionResult Index(string username,string password)
        {
            if (this.HttpContext.Request.Headers.TryGetValue("Authorization", out var tokenstr))
            {
                if (long.TryParse(tokenstr.ToString(), out var token))
                {
                    try
                    {
                        var user = userService.FindUser(token);
                        return Json(new { result = true, token = token, name = user.HumanName, power = user.Power });
                    }
                    catch (Exception)
                    {

                    }
                }

                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                {
                    return Json(new { result = false, info = "无效的token" });
                }
            }

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return Json(new { result = false, info = "帐号或密码不能为空" });
            }

            //获取结果
            try
            {
                var result = loginService.Login(username, password);
                var user = userService.FindUser(result);
                var Result = new { result = true, token = result, name = user.HumanName, power = user.Power };
                return Json(Result);
            }
            catch(Exception e)
            {
                var Result = new { result = false, info = e.Message };
                return Json(Result);
            }
        }

    }
}
