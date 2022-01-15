using Microsoft.EntityFrameworkCore;
using ZwajApp.API.Models;

namespace ZwajApp.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options ):base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}