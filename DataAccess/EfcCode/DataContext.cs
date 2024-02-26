using Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccess.EfcCode
{
    public class DataContext : IdentityDbContext<IdentityUser>
    {
        private readonly IUserIdentityService _identity;

        public DataContext(DbContextOptions<DataContext> options, IUserIdentityService identity) : base(options)
        {
            _identity = identity;
        }

        public DbSet<ToDoList> ToDoLists { get; set; }
        public DbSet<ListTask> ListTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ToDoList>()
                .HasQueryFilter(t => t.UserId == _identity.GetUserId());

            modelBuilder.Entity<ListTask>()
                .HasQueryFilter(t => t.ToDoList.UserId == _identity.GetUserId());
        }

    }
}
