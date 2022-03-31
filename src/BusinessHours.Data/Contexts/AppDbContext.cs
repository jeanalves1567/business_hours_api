using BusinessHours.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessHours.Data.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<BusinessHoursRule> Rules { get; set; }
        public DbSet<BusinessHoursService> Services { get; set; }
        public DbSet<WorkHours> WorkHours { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
