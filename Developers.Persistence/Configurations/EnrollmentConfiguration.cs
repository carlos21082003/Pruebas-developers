using Developers.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Developers.Persistence.Configurations;

public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
{
    public void Configure(EntityTypeBuilder<Enrollment> builder)
    {
        // Nombre la tabla
        builder.ToTable("Enrollments");

        // Clave primaria
        builder.HasKey(x => x.EnrollmentId);

        // Propiedades
        builder.Property(x => x.StudentId).IsRequired();
        builder.Property(x => x.ClassroomId).IsRequired();
        builder.Property(x => x.PreTest).HasPrecision(5,2).IsRequired(false);
        builder.Property(x => x.PostTest).HasPrecision(5,2).IsRequired(false);
        builder.Property(x => x.Passed).HasDefaultValue(true);
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.UpdatedAt).IsRequired();
        builder.Property(x => x.Status).IsRequired();

        // Relaciones
        builder.HasOne(x => x.Classroom)
            .WithMany()
            .HasForeignKey(x => x.ClassroomId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Student)
            .WithMany()
            .HasForeignKey(x => x.StudentId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
