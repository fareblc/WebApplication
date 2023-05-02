using Microsoft.EntityFrameworkCore;

namespace WebApplication.DataAccessLayer
{
    public class WebApplicationContext : DbContext
    {
        public WebApplicationContext(DbContextOptions<WebApplicationContext> options)
            : base(options) { }

        public DbSet<Model.User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Model.User>().ToTable("Users");
            modelBuilder.Entity<Model.User>().HasKey(u => u.Id);
        }
    }
}
