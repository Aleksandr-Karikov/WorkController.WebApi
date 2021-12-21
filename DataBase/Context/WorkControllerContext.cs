using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkController.WebApi.DataBase.Models;

namespace WorkController.WebApi.DataBase.Context
{
    public class WorkControllerContext : DbContext
    {
        public WorkControllerContext(DbContextOptions<WorkControllerContext> options) : base(options)
        {



        }
        public DbSet<ScreenShots> ScreenShots { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Time> Times { get; set; }
        public DbSet<AllowsEmployee> AllowsEmployees { get; set; }
        protected override void OnModelCreating(ModelBuilder model)
        {
            base.OnModelCreating(model);
            model.Entity<Time>().HasAlternateKey(u => new { u.DateTime , u.UserId });
        }
    }
}
