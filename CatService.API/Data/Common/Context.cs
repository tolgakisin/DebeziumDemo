using CatService.API.Common;
using CatService.API.Data.Entities;
using CatService.API.Data.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CatService.API.Data.Common
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Cat> Cats { get; set; }
        public DbSet<Hat> Hats { get; set; }
        public DbSet<CatWithHatView> CatWithHatView { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var fakeEntities = Utils.GetTypesWithCustomAttribute(typeof(BaseEntity).Assembly, typeof(FakeEntityAttribute));

            foreach (Type fakeEntity in fakeEntities)
            {
                modelBuilder.Entity(fakeEntity).HasNoKey().ToView(null);
            }

            base.OnModelCreating(modelBuilder);
        }

        public async Task<IEnumerable<CatWithHatView>> GetCatWithHatView()
        {
            return await this.CatWithHatView.FromSqlRaw(@"
                select 
	                c.Id CatId,
	                c.Name CatName,
	                c.Breed CatBreed,
	                h.Id HatId,
	                h.Name HatName
                from dbo.Cats c left join dbo.Hats h on c.HatId = h.Id").ToListAsync();
        }
    }
}
