using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DataAccess.EfcCode
{
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        private readonly IUserIdentityService _identity;

        public DataContextFactory(IUserIdentityService identity)
        {
            _identity = identity;
        }

        public DataContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var dbContextBuilder = new DbContextOptionsBuilder<DataContext>();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            dbContextBuilder.UseSqlServer(connectionString);

            return new DataContext(dbContextBuilder.Options, _identity);
        }
    }
}
