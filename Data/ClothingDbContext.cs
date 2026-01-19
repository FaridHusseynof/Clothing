using Clothing.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Clothing.Data
{
    public class ClothingDbContext : IdentityDbContext<AppUser>
    {
        public ClothingDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> products { get; set; }
    }
}
