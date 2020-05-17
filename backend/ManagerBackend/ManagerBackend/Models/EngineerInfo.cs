using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ManagerBackend.Models
{
    [Produces("application/json")]
    public class EngineerInfo
    {
        [Key] //主键 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public DateTime Birthday { get; set; }
        public string Education { get; set; }
        public string Hometown { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int Seniority { get; set; }
        public double Wage { get; set; }


    }

}
