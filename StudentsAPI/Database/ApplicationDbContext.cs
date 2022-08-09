using Microsoft.EntityFrameworkCore;
using StudentsAPI.Database.Entities;

namespace StudentsAPI.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<University> Universities { get; set; }

    }
}
