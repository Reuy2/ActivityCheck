using ActivityCheck.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace ActivityCheck.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Activity> Activity { get; set; }

    }
}
