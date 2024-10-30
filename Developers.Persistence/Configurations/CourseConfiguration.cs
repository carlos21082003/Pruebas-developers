using Developers.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Developers.Persistence.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        // Nombre la tabla
        builder.ToTable("Courses");

        // Clave primaria
        builder.HasKey(x => x.CourseId);

        // Atributos
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200)
            .IsUnicode();
        builder.Property(x => x.Description)
            .IsRequired(false)
            .IsUnicode()
            .HasColumnType("text");
        builder.Property(x => x.Hours)
            .HasDefaultValue(0)
            .HasPrecision(5, 2);
        builder.Property(x => x.UpdatedAt)
            .IsRequired();
        builder.Property(x => x.CreatedAt)
            .IsRequired();
        builder.Property(x => x.Status)
            .IsRequired();
        builder.Property(x => x.Theme)
            .IsRequired(false)
            .IsUnicode()
            .HasColumnType("text");
        builder.Property(x => x.Price)
           .HasColumnType("decimal(18,2)");  // Especifica el tipo de columna con precisión y escala
           

        // Relaciones
    }
}
