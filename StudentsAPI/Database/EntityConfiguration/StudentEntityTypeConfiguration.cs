using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentsAPI.Database.Entities;

namespace StudentsAPI.Database.EntityConfiguration
{
    public class StudentEntityTypeConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(x => x.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.LastName)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(x => x.DateOfBirth)
                .HasColumnType("Date")
                .IsRequired();

            builder
                .HasMany(x => x.Universities)
                .WithMany(x => x.Students);
        }
    }
}
