﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Produces("application/json")]
    public class UserController:Controller
    {
        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            this._context = context;
        }


        [HttpGet]
        public List<UserInfo> Get()
        {
            _context.Database.EnsureCreated();
            var users = _context.UserInfo;
            List<UserInfo> items = new List<UserInfo>();
            foreach (var item in users)
            {
                items.Add(item);
            }
            return items;
        }

        [HttpGet]
        public List<UserInfo> GetById(int id)
        {
            _context.Database.EnsureCreated();
            var users = _context.UserInfo.Where(s => s.ID == id).ToList();
            List<UserInfo> items = new List<UserInfo>();
            foreach (var item in users)
            {
                items.Add(item);
            }
            return items;
        }




        [HttpPost]
        public bool Add([FromBody]UserInfo NewUser)
        {
            var state = GetById(NewUser.ID);
            if (state != null)
            {
                return false;
            }
            _context.Add(NewUser);

            return _context.SaveChanges() > 0;

        }


        [HttpPost]
        public bool DeleteById(int id)
        {
            //_context.SaveChanges();
            //return 200;
            var state = false;
            var u = _context.UserInfo.SingleOrDefault(s => s.ID == id);
            if (u != null)
            {
                _context.UserInfo.Remove(u);
                state = _context.SaveChanges() > 0;
            }
            return state;
        }


        [HttpPost]
        public bool Put(int id, [FromBody]UserInfo NewUser)
        {
            var u = _context.UserInfo.Update(NewUser);
            if (u == null)
            {
                return false;
            }
            return _context.SaveChanges() > 0;
        }
    }
}