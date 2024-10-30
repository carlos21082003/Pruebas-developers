using Developers.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Developers.Persistence.Configurations;

public class TrainerConfiguration : IEntityTypeConfiguration<Trainer>
{
    public void Configure(EntityTypeBuilder<Trainer> builder)
    {
        // Nombre la tabla
        builder.ToTable("Trainers");

        // Clave primaria
        builder.HasKey(x => x.TrainerId);

        // Propiedades
        builder.Property(x => x.Dni)
                .HasMaxLength(15) // Longitud
                                  //.HasColumnType("nvarchar") // Tipo de dato
                .IsUnicode() // Soporte caracteres especiales: ñ, ü, 
                .IsRequired(); // Campo requerido
        builder.Property(x => x.LastName).HasMaxLength(100).IsRequired();
        builder.Property(x => x.FirstName).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Email).HasMaxLength(100).IsRequired(false);
        builder.Property(x => x.PhoneNumber).HasMaxLength(50).IsRequired(false);
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.UpdatedAt).IsRequired();
        builder.Property(x => x.Status).IsRequired();
        // Relaciones
    }
}
