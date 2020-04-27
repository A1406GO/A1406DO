using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<EngineerInfo> Engineerinfo { get; set; }
        public DbSet<UserInfo> Userinfo { get; set; }
        public DbSet<ModifyInfo> Modifyinfo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EngineerInfo>();
            modelBuilder.Entity<UserInfo>();
            modelBuilder.Entity<ModifyInfo>();
        }
    }
}
