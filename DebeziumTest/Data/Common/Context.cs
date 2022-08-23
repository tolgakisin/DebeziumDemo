using DebeziumTest.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebeziumTest.Data.Common
{
    public class Context : DbContext
    {
        public virtual DbSet<Hat> Hats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Hat>().ToTable("Hats", t => t.ExcludeFromMigrations());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("CatDb.ConnectionString"));
        }

        public int SaveChangesWithIdentityInsert()
        {
            int change = 0;
            using (var transaction = Database.BeginTransaction())
            {
                try
                {
                    Database.ExecuteSqlInterpolated($"SET IDENTITY_INSERT dbo.Hats ON");
                    change = SaveChanges();
                    Database.ExecuteSqlInterpolated($"SET IDENTITY_INSERT dbo.Hats OFF");
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    change = 0;
                }
            }

            return change;
        }
    }
}
