using Microsoft.EntityFrameworkCore;
using PharmaProject.Models;

namespace PharmaProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<UsersDTO> Users { get; set; }
    }
}
