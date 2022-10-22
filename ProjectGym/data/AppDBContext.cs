using Microsoft.EntityFrameworkCore;

namespace ProjectGym.data
{
    public class AppDBContext:DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options):base(options){}
        public DbSet<Coach> Coaches { get; set; }
    }
}
