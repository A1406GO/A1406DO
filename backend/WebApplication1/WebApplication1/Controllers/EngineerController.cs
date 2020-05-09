using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Produces("application/json")]
    //[Route("api/Engineer")]
    public class EngineerController: Controller
    {
        private readonly DataContext _context;

        public EngineerController(DataContext context)
        {
            this._context = context;
        }


        [HttpGet]
        public List<EngineerInfo> Get()
        {
            _context.Database.EnsureCreated();
            var users = _context.EngineerInfo;
            List<EngineerInfo> items = new List<EngineerInfo>();
            foreach (var item in users)
            {
                items.Add(item);
            }
            return items;
        }


        [HttpGet]
        public List<EngineerInfo> GetById(int id)
        {
            _context.Database.EnsureCreated();
            var users = _context.EngineerInfo.Where(s => s.ID == id).ToList(); 
            List<EngineerInfo> items = new List<EngineerInfo>();
            foreach (var item in users)
            {
                items.Add(item);
            }
            return items;
        }


        [HttpGet]
        public List<EngineerInfo> GetByName(string name)
        {
            _context.Database.EnsureCreated();
            var users = _context.EngineerInfo.Where(s => s.Name == name).ToList();
            List<EngineerInfo> items = new List<EngineerInfo>();
            foreach (var item in users)
            {
                items.Add(item);
            }
            return items;
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
        public bool Add([FromBody]EngineerInfo Newengineer)
        {
            var state = GetById(Newengineer.ID);
            if (state.Count != 0) 
            {
                return false;
            }
            _context.Add(Newengineer);

            return _context.SaveChanges() > 0;

        }

        ///*https://localhost:5001/Engineer/IdAdd?id=1000&name=马佳进&sex=男&brithday=1999-01-01&education=本科&hometown=浙江&address=衢州&phonenumber=17788579131&seniority=1&wage=2000.5
        //     */
        //public bool IdAdd(int id, string name, string sex, string brithday, string education,
        //    string hometown, string address, string phonenumber, int seniority, Double wage)
        //{

        //    var state = GetsById(id);
        //    if(state!=null)
        //    {
        //        return false;
        //    }

        //    DateTime _brithday;
        //    DateTime.TryParse(brithday, out _brithday);
        //    EngineerInfo Newengineer = new EngineerInfo();
        //    //if()
        //    Newengineer.ID = id;
        //    Newengineer.Name = name;
        //    Newengineer.Sex = sex;
        //    Newengineer.Birthday = _brithday;
        //    Newengineer.Education = education;
        //    Newengineer.Hometown = hometown;
        //    Newengineer.Address = address;
        //    Newengineer.PhoneNumber = phonenumber;
        //    Newengineer.Seniority = seniority;
        //    Newengineer.Wage = wage;

        //    _context.Add(Newengineer);

        //    return _context.SaveChanges() > 0; 

        //}







        //https://localhost:5001/Engineer/DeleteById?id=10

        [HttpPost]
        public bool DeleteById(int id)
        {
            //_context.SaveChanges();
            //return 200;
            var state = false;
            var u = _context.EngineerInfo.SingleOrDefault(s => s.ID == id);
            if(u!=null)
            {
                _context.EngineerInfo.Remove(u);
                state = _context.SaveChanges() > 0;
            }
            return state;
        }


        [HttpPost]
        public bool DeleteAll()
        {
            //_context.SaveChanges();
            //return 200;
            var state = false;
            var users = _context.EngineerInfo.ToList();

            foreach (var item in users)
            {
                _context.EngineerInfo.Remove(item);
                state = _context.SaveChanges() > 0;
            }
            return state;
        }

        //https://localhost:5001/Engineer/Put?id=11
        [HttpPost]
        public bool Put(int id,[FromBody]EngineerInfo Newengineer)
        {
            //var state = false;
            var u = _context.EngineerInfo.Update(Newengineer);
            if(u==null)
            {
                return false;
            }
            return _context.SaveChanges() > 0;
        }


        //[HttpPost]
        ////https://localhost:5001/Engineer/DeleteByName?name=周则千
        //public string DeleteByName(string name)
        //{
        //    var u = _context.Engineerinfo.Where(s => s.Name==name).ToList();
        //    //_context.SaveChanges();
        //    //return 200;
        //    try
        //    {
        //        foreach (var item in u)
        //        {
        //            _context.Engineerinfo.Remove(item);
        //        }
        //        _context.SaveChanges();
        //    }
        //    catch (SqlException ex)
        //    {
        //        return "fail";
        //    }
        //    return "succeed";
        //}
    }
}
