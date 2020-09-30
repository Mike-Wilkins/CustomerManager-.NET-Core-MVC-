using CustomerManager.Models;
using CustomerManager.Services;
using Microsoft.EntityFrameworkCore;

namespace CustomerManager.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {

        public DbSet<Customer> Customers { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
    }
}
