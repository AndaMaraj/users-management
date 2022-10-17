using Microsoft.EntityFrameworkCore;

using UsersManagement.Repository.Entities;

namespace UsersManagement.Repository
{
    public class UsersDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public UsersDbContext(DbContextOptions options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<Role>()
                .HasQueryFilter(x => x.IsDeleted == false);
        }
    }
}
