using Microsoft.EntityFrameworkCore;

namespace ToDos.Data
{
    public class ListContext : DbContext
    {
        public ListContext(DbContextOptions<ListContext> options) : base(options){}

    }    
}