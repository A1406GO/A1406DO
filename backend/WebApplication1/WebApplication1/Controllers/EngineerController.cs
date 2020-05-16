using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Models.Services;
using WebApplication1.Extensions;

namespace WebApplication1.Controllers
{
    [Produces("application/json")]
    //[Route("api/Engineer")]
    public class EngineerController: Controller
    {
        private readonly DataContext _context;
        private UserService userservice;
        private Modify modify;

        public EngineerController(DataContext context, UserService userservice, Modify modify)
        {
            this._context = context;
            this.userservice = userservice;
            this.modify = modify;
        }


        [HttpGet]
        public IActionResult Get()
        {

            var user = this.GetAuthUser();
            if (user.Power != 1)
            {
                return StatusCode(403);
            }

            _context.Database.EnsureCreated();
            var users = _context.EngineerInfo.ToList();
            return Json(users);
        }


        [HttpGet]
        public IActionResult GetById(int id)
        {
            var user = this.GetAuthUser();
            if (user.Power != 1)
            {
                return StatusCode(403);
            }


            _context.Database.EnsureCreated();
            var users = _context.EngineerInfo.Where(s => s.ID == id).ToList(); 
            List<EngineerInfo> items = new List<EngineerInfo>();
            foreach (var item in users)
            {
                items.Add(item);
            }
            return Json(items);
        }


        [HttpGet]
        public IActionResult GetByName(string name)
        {
            var user = this.GetAuthUser();
            if (user.Power != 1)
            {
                return StatusCode(403);
            }


            _context.Database.EnsureCreated();
            var users = _context.EngineerInfo.Where(s => s.Name == name).ToList();
            List<EngineerInfo> items = new List<EngineerInfo>();
            foreach (var item in users)
            {
                items.Add(item);
            }
            return Json(items);
        }

        [HttpPost]
        public List<EngineerInfo> SortById([FromBody]List<EngineerInfo> Newengineer)
        {
            List<EngineerInfo> items = Newengineer;
            if(items!=null)
            {
                items = items.OrderBy(u => u.ID).ToList();
            }
            return items;
        }

        [HttpPost]
        public List<EngineerInfo> SortByName([FromBody]List<EngineerInfo> Newengineer)
        {
            List<EngineerInfo> items = Newengineer;
            if (items != null)
            {
                items = items.OrderBy(u => u.Name).ThenBy( u => u.ID ).ToList();
            }
            return items;
        }


        [HttpPost]
        public List<EngineerInfo> SortBySeniority([FromBody]List<EngineerInfo> Newengineer)
        {
            List<EngineerInfo> items = Newengineer;
            if (items != null)
            {
                items = items.OrderByDescending(u => u.Seniority).ThenBy(u => u.ID).ToList();
            }
            return items;
        }




        /*https://localhost:5001/Engineer/Add?name=马佳进&sex=男&brithday=1999-01-01&education=本科&hometown=浙江&address=衢州&phonenumber=17788579131&seniority=1&wage=2000.5
             */
        [HttpPost]
        public IActionResult Add([FromBody]EngineerInfo Newengineer)
        {
            var user = this.GetAuthUser();
            if (user.Power != 1)
            {
                return StatusCode(403);
            }




            try
            {
                _context.Add(Newengineer);
                _context.SaveChanges();
            }
            catch(Exception e)
            {
                return Json(new { success = false });
            }
            //成功时执行以下操作
            //获取header中的token
            var providedApiKey = long.Parse(Request.Headers["Authorization"].ToString());
            //获取日志信息
            ModifyInfo NewModify = modify.AddInfo(DateTime.Now, "Engineer", 1, providedApiKey); 
            //保存日志信息
            _context.Add(NewModify);

            return Json(new { success = _context.SaveChanges() > 0 });
        }




        [HttpPost]
        public IActionResult DeleteById(int id)
        {
            var state = false;
            var u = _context.EngineerInfo.SingleOrDefault(s => s.ID == id);
            var user = this.GetAuthUser();
            if (user.Power != 1)
            {
                return StatusCode(403);
            }





            if (u != null)
            {
                _context.EngineerInfo.Remove(u);
                state = _context.SaveChanges() > 0;
            }

            if (state == true)
            {
                //成功时执行以下操作
                //获取header中的token
                var providedApiKey = long.Parse(Request.Headers["Authorization"].ToString());
                //获取日志信息
                ModifyInfo NewModify = modify.DeleteInfo(DateTime.Now, "Engineer", 1, providedApiKey);
                //保存日志信息
                _context.Add(NewModify);
            }

            return Json(new { success = state });

        }


        [HttpPost]
        public IActionResult DeleteAll()
        {
            var state = false;
            var users = _context.EngineerInfo.ToList();

            var user = this.GetAuthUser();
            if (user.Power != 1)
            {
                return StatusCode(403);
            }



            foreach (var item in users)
            {
                _context.EngineerInfo.Remove(item);
                state = _context.SaveChanges() > 0;
            }

            if (state == true)
            {
                //成功时执行以下操作
                //获取header中的token
                var providedApiKey = long.Parse(Request.Headers["Authorization"].ToString());
                //获取日志信息
                ModifyInfo NewModify = modify.DeleteInfo(DateTime.Now, "Engineer", users.Count(), providedApiKey);
                //保存日志信息
                _context.Add(NewModify);
            }

            return Json(new { success = state });
        }

        //https://localhost:5001/Engineer/Put?id=11
        [HttpPost]
        public IActionResult UpdataById(int id,[FromBody]EngineerInfo Newengineer)
        {
            var user = this.GetAuthUser();
            if (user.Power != 1)
            {
                return StatusCode(403);
            }

            var u = _context.EngineerInfo.Update(Newengineer);
            if (u == null) 
            {
                return Json(new { success = false });
            }

            //成功时执行以下操作
            //获取header中的token
            var providedApiKey = long.Parse(Request.Headers["Authorization"].ToString());
            //获取日志信息
            ModifyInfo NewModify = modify.UpdataInfo(DateTime.Now, "Engineer", 1, providedApiKey);
            //保存日志信息
            _context.Add(NewModify);

            return Json(new { success = _context.SaveChanges() > 0 });
        }


        [HttpPost]
        public IActionResult DataChange([FromBody]ChangedEngineerData Data)
        {
            var user = this.GetAuthUser();
            if (user.Power != 1)
            {
                return StatusCode(403);
            }



            //分别获取增加，删除，更新数据的信息
            List<EngineerInfo> AddengineerInfos = Data.Add;
            List<EngineerInfo> DeleteengineerInfos = Data.Delete;
            List<EngineerInfo> UpdataengineerInfos = Data.Update;
            ModifyInfo NewModify;
            try
            {
                //获取header中的token
                var providedApiKey = long.Parse(Request.Headers["Authorization"].ToString());
                //添加数据
                if(AddengineerInfos!=null&&AddengineerInfos.Count!=0)
                {
                    foreach (var item in AddengineerInfos)
                    {
                        _context.EngineerInfo.Add(item);
                    }
                    //获取日志信息
                    NewModify = modify.AddInfo(DateTime.Now, "Engineer", AddengineerInfos.Count(), providedApiKey);
                    //保存日志信息
                    _context.ModifyInfo.Add(NewModify);
                }
                //删除数据
                if(DeleteengineerInfos != null&&DeleteengineerInfos.Count != 0)
                {
                    foreach (var item in DeleteengineerInfos)
                    {
                        var Deletedata = _context.EngineerInfo.SingleOrDefault(s => s.ID == item.ID);
                        _context.EngineerInfo.Remove(Deletedata);
                    }
                    NewModify = modify.DeleteInfo(DateTime.Now, "Engineer", DeleteengineerInfos.Count(), providedApiKey);
                    _context.ModifyInfo.Add(NewModify);
                }
                //更新数据
                if(UpdataengineerInfos!= null&&UpdataengineerInfos.Count != 0 )
                {
                    foreach (var item in UpdataengineerInfos)
                    {
                        _context.EngineerInfo.Update(item);
                    }
                    //获取并保存更新数据的日志的信息
                    NewModify = modify.UpdataInfo(DateTime.Now, "Engineer", UpdataengineerInfos.Count(), providedApiKey);
                    _context.ModifyInfo.Add(NewModify);
                }

                //保存数据，如果以上有一个出错那么全部不执行
                _context.SaveChanges();
            }

            catch (Exception e)
            {
                return Json(new { success = false ,errorinfo = e.ToString()});
            }
            return Json(new { success = true });
        }

    }
}
