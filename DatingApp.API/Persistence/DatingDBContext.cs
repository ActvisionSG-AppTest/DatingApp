using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Persistence
{
    public class DatingDBContext : DbContext
    {
        public DatingDBContext(DbContextOptions<DatingDBContext> options) : base(options) { }

        public DbSet<Value> Values { get; set; }
    }
}