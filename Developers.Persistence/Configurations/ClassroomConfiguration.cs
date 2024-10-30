using Developers.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Developers.Persistence.Configurations;

public class ClassroomConfiguration : IEntityTypeConfiguration<Classroom>
{
    public void Configure(EntityTypeBuilder<Classroom> builder)
    {
        // Nombre la tabla
        builder.ToTable("Classrooms");

        // Clave primaria
        builder.HasKey(x => x.ClassroomId);

        // Propiedades
        builder.Property(x => x.SessionDate).HasColumnType("date").IsRequired(); 
        builder.Property(x => x.Hours).HasPrecision(5,2).IsRequired();
        builder.Property(x => x.Details).HasColumnType("text").IsRequired(false);
        builder.Property(x => x.TrainerId).IsRequired();
        builder.Property(x => x.CourseId).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.UpdatedAt).IsRequired();
        builder.Property(x => x.Status).IsRequired();

        // Relaciones
        builder.HasOne(x => x.Trainer)
            .WithMany()
            .HasForeignKey(x => x.TrainerId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Course)
            .WithMany()
            .HasForeignKey(x => x.CourseId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
