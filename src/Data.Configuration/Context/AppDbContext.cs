using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Data.Configuration.EntityType;
using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data.Configuration.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Product { get; set; } = default!;
        public DbSet<User> User { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductEntityType());
            modelBuilder.ApplyConfiguration(new UserEntityType());

            base.OnModelCreating(modelBuilder);
        }
    }
}
