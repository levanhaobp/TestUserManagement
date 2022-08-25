using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Contexts
{
    internal class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        public ApplicationContext CreateDbContext(string[] args)
        {
            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{envName}.json", optional: false)
                .Build();

            var optionBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            var connectionString = config.GetConnectionString("ConnectionDefault");
            optionBuilder.UseSqlServer(connectionString);

            return new ApplicationContext(optionBuilder.Options);
        }
    }
}
