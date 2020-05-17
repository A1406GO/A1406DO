using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ManagerBackend.Models
{
    [Produces("application/json")]
    public class EngineerInfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
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

        //EngineerInfo()
        //public EngineerInfo(int id, string name, string sex, DateTime brithday, string education,
        //    string hometown, string address, string phonenumber, int seniority, Double wage)
        //{
        //    ID = id;
        //    Name = name;
        //    Sex = sex;
        //    Birthday = brithday;
        //    Education = education;
        //    Hometown = hometown;
        //    Address = address;
        //    PhoneNumber = phonenumber;
        //    Seniority = seniority;
        //    Wage = wage;
        //}
    }

}
