using Microsoft.EntityFrameworkCore;

namespace DataAccess.EfcCode
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
    }
}
