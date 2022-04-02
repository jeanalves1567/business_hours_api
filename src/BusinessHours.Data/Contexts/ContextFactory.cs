using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BusinessHours.Data.Contexts
{
    public class ContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            // var connectionString = "Server=acme.com,1433;Initial Catalog=BusinessHoursApi;User ID=sa;Password=pa55w0rd!;";
            var connectionString = "server=acme.com;port=3306;database=business_hours_api;user=root;Password=pa55w0rd!;Persist Security Info=False;Connect Timeout=300";
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            // optionsBuilder.UseSqlServer(connectionString);
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
