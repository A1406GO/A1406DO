using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class EngineerInfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public string Birthday { get; set; }
        public string Education { get; set; }
        public string Hometown { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int Seniority { get; set; }
        public double Wage { get; set; }
    }

    public class UserInfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ControllerName { get; set; }
        public int Power { get; set; }
    }
    public class ModifyInfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public string Time { get; set; }
        public string Username { get; set; }
        public string Modify { get; set; }
    }
}
