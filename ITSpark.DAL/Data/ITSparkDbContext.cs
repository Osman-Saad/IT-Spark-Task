using ITSpark.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ITSpark.DAL.Data
{
    public class ITSparkDbContext:IdentityDbContext
    {
        public ITSparkDbContext(DbContextOptions<ITSparkDbContext> dbContextOptions):base(dbContextOptions)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Item> Items { get; set; }
    }
}
