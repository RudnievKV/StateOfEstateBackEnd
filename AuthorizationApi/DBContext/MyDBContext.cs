using AuthorizationApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthorizationApi.DBContext
{
    public class MyDBContext : DbContext
    {
        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; init; }
        public DbSet<UserType> UserTypes { get; init; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasOne(x => x.UserType)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.UserType_ID);
        }
    }
}
