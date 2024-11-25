using System.ComponentModel.DataAnnotations;

namespace Developers.Models;

public class Inscripciones: EntityBase
{
    public int InscripcionesId { get; set; }
    [DataType(DataType.Date)]
    public DateTime SessionDateInscripciones { get; set; }
    public decimal HoursInscripciones { get; set; }
    public string? DetailsInscripciones { get; set; }


    //Relaciones: Trainer, Course --> Uno a muchos inversa
    public int StudentId { get; set; }
    public Student? Student { get; set; }
    public int CourseId { get; set; }
    public Course? Course { get; set; }

    // UpdatedAt
    // CreatedAt
    // Status
}
