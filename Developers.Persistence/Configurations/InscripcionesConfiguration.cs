using Developers.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Developers.Persistence.Configurations;

public class InscripcionesConfiguration : IEntityTypeConfiguration<Inscripciones>
{
    public void Configure(EntityTypeBuilder<Inscripciones> builder)
    {
        // Nombre la tabla
        builder.ToTable("Inscripciones");

        // Clave primaria
        builder.HasKey(x => x.InscripcionesId);

        // Propiedades
        builder.Property(x => x.SessionDateInscripciones).HasColumnType("date").IsRequired();
        builder.Property(x => x.HoursInscripciones).HasPrecision(5, 2).IsRequired();
        builder.Property(x => x.DetailsInscripciones).HasColumnType("text").IsRequired(false);
        builder.Property(x => x.StudentId).IsRequired();
        builder.Property(x => x.CourseId).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.UpdatedAt).IsRequired();
        builder.Property(x => x.Status).IsRequired();

        // Relaciones
        builder.HasOne(x => x.Student)
            .WithMany()
            .HasForeignKey(x => x.StudentId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Course)
            .WithMany()
            .HasForeignKey(x => x.CourseId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
