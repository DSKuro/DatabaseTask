using DatabaseTask.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace DatabaseTask.Services.Database
{
    public class ApplicationContext :DbContext
    {
        public DbSet<DrawingContents> Contents => Set<DrawingContents>();
    
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
