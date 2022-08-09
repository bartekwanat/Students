using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentsAPI.Database.Entities;

namespace StudentsAPI.Database.EntityConfiguration
{
    public class StudentEntityTypeConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder
                .Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(x => x.DateOfBirth)
                .IsRequired()
                .HasColumnType("date");

            builder
                .Property(x => x.UniversityId)
                .IsRequired()
                .HasColumnType("UNIQUEIDENTIFIER");
            
            builder
                .HasOne(x => x.University)
                .WithMany(x => x.Students)
                .HasForeignKey(x => x.UniversityId);
        }
    }
}
