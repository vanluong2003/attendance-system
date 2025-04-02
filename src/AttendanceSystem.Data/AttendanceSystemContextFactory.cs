using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AttendanceSystem.Data
{
    public class AttendanceSystemContextFactory : IDesignTimeDbContextFactory<AttendanceSystemContext>
    {
        public AttendanceSystemContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<AttendanceSystemContext>();
            builder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            return new AttendanceSystemContext(builder.Options);
        }
    }
}
