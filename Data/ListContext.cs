using Microsoft.EntityFrameworkCore;
using ToDos.Entity;

namespace ToDos.Data
{
    public class ListContext : DbContext
    {
        public ListContext(DbContextOptions<ListContext> options) : base(options){}
        public DbSet<User> Users { get; set; }
    }    
}