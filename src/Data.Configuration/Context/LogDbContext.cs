using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data.Configuration.Context
{
    public class LogDbContext : DbContext
    {
        public LogDbContext(DbContextOptions<LogDbContext> options) : base(options)
        {
        }
        public DbSet<Log> Log { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Log>().ToTable("log");
            modelBuilder.Entity<Log>().HasData(
                new Log { Id = 1, MachineName = "HOST1", Logged = new DateTime(2025, 4, 10), Level = "ERROR", Message = "", ApplicationId = 2 },
                new Log { Id = 2, MachineName = "HOST2", Logged = new DateTime(2025, 4, 10), Level = "FATAL", Message = "", ApplicationId = 1 },
                new Log { Id = 3, MachineName = "HOST3", Logged = new DateTime(2025, 4, 10), Level = "WARNING", Message = "", ApplicationId = 2 },
                new Log { Id = 4, MachineName = "HOST4", Logged = new DateTime(2025, 4, 10), Level = "FATAL", Message = "", ApplicationId = 1 },
                new Log { Id = 5, MachineName = "HOST5", Logged = new DateTime(2025, 4, 10), Level = "ERROR", Message = "", ApplicationId = 2 },
                new Log { Id = 6, MachineName = "HOST6", Logged = new DateTime(2025, 4, 10), Level = "WARNING", Message = "", ApplicationId = 1 }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
