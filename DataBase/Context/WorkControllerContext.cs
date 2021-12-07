using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiWorkControllerServer.Models;
using WorkController.WebApi.DataBase.Models;

namespace WebApiWorkControllerServer.Context
{
    public class WorkControllerContext : DbContext
    {
        public WorkControllerContext(DbContextOptions<WorkControllerContext> options) : base(options)
        {


            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<AllowsEmployee> AllowsEmployees { get; set; }
        protected override void OnModelCreating(ModelBuilder model)
        {
            base.OnModelCreating(model);
        }
    }
}
