using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentsAPI.Database.Entities;

namespace StudentsAPI.Database.EntityConfiguration
{
    public class UniversityEntityTypeConfiguration : IEntityTypeConfiguration<University>
    {
        public void Configure(EntityTypeBuilder<University> builder)
        {

        }
    }
}
