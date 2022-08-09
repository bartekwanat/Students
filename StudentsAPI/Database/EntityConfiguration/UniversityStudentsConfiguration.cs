using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentsAPI.Database.Entities;

namespace StudentsAPI.Database.EntityConfiguration
{
    public class UniversityStudentsConfiguration : IEntityTypeConfiguration<UniversityStudents>
    {
        public void Configure(EntityTypeBuilder<UniversityStudents> builder)
        {
            builder.HasKey(x => new {x.StudentId, x.UniversityId});

            builder.HasOne(x => x.Student)
                .WithMany(x => x.UniversityStudents)
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.University)
                .WithMany(x => x.UniversityStudents)
                .HasForeignKey(x => x.UniversityId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
