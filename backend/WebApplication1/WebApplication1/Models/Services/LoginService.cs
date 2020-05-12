using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Models.Services;

namespace WebApplication1.Services
{
    public class LoginService
    {
        //设置登录类
        //设置登录类所需的变量
        private Random random;
        private readonly DataContext _context;
        private UserService loginedUsers;
        //初始化
        public LoginService(DataContext context, UserService loginedUsers)
        {
            random = new Random(DateTime.Now.Millisecond);
            this.loginedUsers = loginedUsers;
            this._context = context;
        }
        //登录函数，输入名字密码，如果正确返回一个token，否则反正错误的原因
        public string Login(string username,string password)
        {
            _context.Database.EnsureCreated();
            //获取账号
            var user = _context.UserInfo.Where(s => s.UserName == username).FirstOrDefault();
            //失败的情况
            if(user == null)
            {
                throw new Exception("用户名不存在！");
            }
            if(user.Password != password)
            {
                throw new Exception("密码不正确！");
            }
            if(loginedUsers.ContainsUser(user))
            {
                throw new Exception("此用户已登录！");
            }
            //成功则随机生成token
            long token = DateTime.Now.Ticks;
            while (loginedUsers.ContainsToken(token))
            {
                token ^= random.Next();
            }
            loginedUsers.Add(token, user);
            return token.ToString();
        }

        //public bool ValidToken(long token)
        //{
        //    return loginedUsers.ContainsKey(token);
        //}


    }
}
