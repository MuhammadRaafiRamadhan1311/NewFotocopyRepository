using Microsoft.EntityFrameworkCore;

namespace Fotocopy.Data
{
    public class FotocopyContext : DbContext
    {
        public FotocopyContext(DbContextOptions<FotocopyContext> options) 
            : base(options)
        { 
            
        }

        public DbSet<Paper> Paper {  get; set; }

    }
}
