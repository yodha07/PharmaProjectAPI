using Microsoft.EntityFrameworkCore;
using PharmaProjectAPI.Models;

namespace PharmaProjectAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

    }
}
