using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Models.Services;
using WebApplication1.Extensions;


namespace WebApplication1.Controllers
{
    [Produces("application/json")]
    public class UserController:Controller
    {
        private readonly DataContext _context;
        private UserService userservice;
        private Modify modify;

        public UserController(DataContext context, UserService userservice, Modify modify)
        {
            this._context = context;
            this.userservice = userservice;
            this.modify = modify;
        }


        [HttpGet]
        public IActionResult Get()
        {

            var user = this.GetAuthUser();
            if (user.Power != 2)
            {
                return StatusCode(401);
            }

            _context.Database.EnsureCreated();
            var users = _context.UserInfo;
            List<UserInfo> items = new List<UserInfo>();
            foreach (var item in users)
            {
                items.Add(item);
            }
            return Json(items);
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {

            var user = this.GetAuthUser();
            if (user.Power != 2)
            {
                return StatusCode(401);
            }
            _context.Database.EnsureCreated();
            var users = _context.UserInfo.Where(s => s.ID == id).ToList();
            List<UserInfo> items = new List<UserInfo>();
            foreach (var item in users)
            {
                items.Add(item);
            }
            return Json(items);
        }




        [HttpPost]
        public IActionResult Add([FromBody]UserInfo NewUser)
        {
            var state = GetById(NewUser.ID);
            if (state != null)
            {
                return Json(new { sucess = false });
            }


            var user = this.GetAuthUser();
            if (user.Power != 2)
            {
                return StatusCode(401);
            }





            _context.Add(NewUser);

            try
            {
                var providedApiKey = long.Parse(Request.Headers["Authorization"].ToString());
                //获取日志信息
                ModifyInfo NewModify = modify.AddInfo(DateTime.Now, "User", 1, providedApiKey);
                //保存日志信息
                _context.Add(NewModify);
                _context.SaveChanges();
            }
            catch(Exception e)
            {
                return Json(new { sucess = false });
            }

            return Json(new { sucess = true });

        }


        [HttpPost]
        public IActionResult DeleteById(int id)
        {
            var state = false;
            var user = this.GetAuthUser();
            if (user.Power != 2)
            {
                return StatusCode(401);
            }
            var u = _context.UserInfo.SingleOrDefault(s => s.ID == id);
            if (u != null)
            {
                _context.UserInfo.Remove(u);
                state = _context.SaveChanges() > 0;
            }

            if(state==true)
            {
                var providedApiKey = long.Parse(Request.Headers["Authorization"].ToString());
                //获取日志信息
                ModifyInfo NewModify = modify.DeleteInfo(DateTime.Now, "User", 1, providedApiKey);
                //保存日志信息
                _context.Add(NewModify);
            }

            return Json(new { sucess = state });
        }


        [HttpPost]
        public IActionResult UpdataById(int id, [FromBody]UserInfo NewUser)
        {
            var u = _context.UserInfo.Update(NewUser);

            var user = this.GetAuthUser();
            if (user.Power != 2)
            {
                return StatusCode(401);
            }

            if (u == null)
            {
                return Json(new { sucess = false });
            }


            try
            {
                var providedApiKey = long.Parse(Request.Headers["Authorization"].ToString());
                //获取日志信息
                ModifyInfo NewModify = modify.UpdataInfo(DateTime.Now, "User", 1, providedApiKey);
                //保存日志信息
                _context.Add(NewModify);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return Json(new { sucess = false });
            }

            return Json(new { sucess = true });
        }



        [HttpPost]
        public IActionResult DataChange([FromBody]ChangedUserData Data)
        {
            var user = this.GetAuthUser();
            if (user.Power != 2)
            {
                return StatusCode(401);
            }
            //分别获取增加，删除，更新数据的信息
            List<UserInfo> AdduserInfos = Data.AdduserInfos;
            List<UserInfo> DeleteuserInfos = Data.DeleteuserInfos;
            List<UserInfo> UpdatauserInfos = Data.UpdatauserInfos;
            try
            {
                //获取token
                var providedApiKey = long.Parse(Request.Headers["Authorization"].ToString());
                //添加数据
                foreach (var item in AdduserInfos)
                {
                    _context.UserInfo.Add(item);
                }
                //删除数据
                foreach (var item in DeleteuserInfos)
                {
                    var Deletedata = _context.UserInfo.SingleOrDefault(s => s.ID == item.ID);
                    _context.UserInfo.Remove(Deletedata);
                }
                //更新数据
                foreach (var item in UpdatauserInfos)
                {
                    _context.UserInfo.Update(item);
                }
               
                //获取增加数据的日志信息
                ModifyInfo NewModify = modify.AddInfo(DateTime.Now, "User", AdduserInfos.Count(), providedApiKey);
                //日志信息加入
                _context.Add(NewModify);

                //获取删除数据的日志信息
                NewModify = modify.DeleteInfo(DateTime.Now, "Engineer", DeleteuserInfos.Count(), providedApiKey);
                //日志信息加入
                _context.Add(NewModify);

                //获取更新数据的日志信息
                NewModify = modify.UpdataInfo(DateTime.Now, "Engineer", UpdatauserInfos.Count(), providedApiKey);
                //日志信息加入
                _context.Add(NewModify);
                //保存数据，如果以上有一个出错那么全部不执行
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return Json(new { success = false });
            }
            return Json(new { success = true });
        }
    }
}
