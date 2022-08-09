using Microsoft.EntityFrameworkCore;
using StudentsAPI.Database.Entities;

namespace StudentsAPI.Database
{
    public interface IApplicationDbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<University> Universities { get; set; }
        Task <int> SaveChangesAsync();
    }
}
