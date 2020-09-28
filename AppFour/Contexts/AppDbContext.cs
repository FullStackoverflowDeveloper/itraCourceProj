using AppFour.Models.Collection;
using AppFour.Models.Entrance;
using AppFour.Models.Fields;
using AppFour.Models.Item;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppFour.Cotexts
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<CustomField> CustomFields { get; set; }
        public DbSet<FieldData> FieldsData { get; set; }
        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
