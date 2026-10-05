using Microsoft.EntityFrameworkCore;
using WebApplication_CRUD.Model;

namespace WebApplication_CRUD.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(
            DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

    }
}
