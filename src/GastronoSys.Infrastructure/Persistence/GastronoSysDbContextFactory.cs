using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace GastronoSys.Infrastructure.Persistence
{
    public class GastronoSysDbContextFactory : IDesignTimeDbContextFactory<GastronoSysDbContext>
    {
        public GastronoSysDbContext CreateDbContext(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
            var basePath = Directory.GetCurrentDirectory();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<GastronoSysDbContext>();

            optionsBuilder.UseSqlServer(connectionString);

            return new GastronoSysDbContext(optionsBuilder.Options);
        }
    }
}
