using Microsoft.EntityFrameworkCore;
using TestTaskWishListAPI.Models;

namespace TestTaskWishListAPI.Data
{
    public class DBContext : DbContext
    {
        public DbSet<WishItem> wishItems { get; set; }

        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WishItem>()
                .HasKey(w => w.Id);
            modelBuilder.Entity<WishItem>()
                .Property(w => w.Id)
                .ValueGeneratedOnAdd(); 
        }

        public override int SaveChanges()
        {
            var addedEntities = ChangeTracker.Entries<WishItem>()
                .Where(e => e.State == EntityState.Added);

            foreach (var entity in addedEntities)
            {
                entity.Entity.DateTimeAdd = DateTime.UtcNow;
            }

            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var addedEntities = ChangeTracker.Entries<WishItem>()
                .Where(e => e.State == EntityState.Added);

            foreach (var entity in addedEntities)
            {
                entity.Entity.DateTimeAdd = DateTime.UtcNow;
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}
