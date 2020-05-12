using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Services;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    //登录时调用的函数类
    [Produces("application/json")]
    public class LoginController: Controller
    {
        //设置自己的变量登录服务类
        private LoginService loginService;
        //初始化，将内存中的登录服务类加载进入自己的变量中
        public LoginController(LoginService loginService)
        {
            this.loginService = loginService;
        }
        //真正的调用的判断函数，返回的是json
        //https://localhost:5001/Login/?username=1&password=1
        [HttpGet]
        public IActionResult Index(string username,string password)
        {
            //获取结果
            try
            {
                var result = loginService.Login(username, password);
                var Result = new { result = true, token = result };
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
