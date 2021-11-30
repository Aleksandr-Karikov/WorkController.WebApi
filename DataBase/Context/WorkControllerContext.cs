using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiWorkControllerServer.Models;

namespace WebApiWorkControllerServer.Context
{
    public class WorkControllerContext : DbContext
    {
        public WorkControllerContext(DbContextOptions<WorkControllerContext> options) : base(options)
        {


            
        }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder model)
        {
            base.OnModelCreating(model);
        }
    }
}
