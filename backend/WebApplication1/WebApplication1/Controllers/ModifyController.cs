using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Produces("application/json")]
    public class ModifyController : Controller
    {
        private readonly DataContext _context;

        public ModifyController(DataContext context)
        {
            this._context = context;
        }

        [HttpPost]
        public bool Add([FromBody]ModifyInfo NewModify)
        {
            //var state = GetById(Newengineer.ID);
            if (NewModify == null)
            {
                return false;
            }
            _context.Add(NewModify);

            return _context.SaveChanges() > 0;

        }


    }
}
