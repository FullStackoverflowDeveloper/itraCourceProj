using AppFour.Models.Collection;
using AppFour.Models.Entrance;
using AppFour.Models.Item;
using AppFour.Models.Tag;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppFour.Cotexts
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Collection> Collections { get; set; } 
        public DbSet<CustomField> CustomFields { get; set; }
        public DbSet<CustomFieldData> CustomFieldsData { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Item>()
            //    .HasOne(c => c.Collection)
            //    .WithMany(i => i.Items)
            //    .OnDelete(DeleteBehavior.Cascade);
            //modelBuilder.Entity<Collection>()
            //    .HasOne(u => u.User)
            //    .WithMany(c => c.Collections)
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
