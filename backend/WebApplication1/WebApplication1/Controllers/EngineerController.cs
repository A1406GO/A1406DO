using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Produces("application/json")]
    public class EngineerController: ControllerBase
    {
        private readonly DataContext _context;

        public EngineerController(DataContext context)
        {
            this._context = context;
        }

        public List<EngineerInfo> Gets()
        {
            _context.Database.EnsureCreated();
            var users = _context.Engineerinfo;
            List<EngineerInfo> items = new List<EngineerInfo>();
            foreach (var item in users)
            {
                items.Add(item);
            }
            return items;
        }
    }
}
