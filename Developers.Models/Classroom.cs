using System.ComponentModel.DataAnnotations;

namespace Developers.Models;

public class Classroom:EntityBase
{
    public int ClassroomId { get; set; }
    [DataType(DataType.Date)]
    public DateTime SessionDate { get; set; }
    public decimal Hours { get; set; }
    public string? Details { get; set; }


    //Relaciones: Trainer, Course --> Uno a muchos inversa
    public int TrainerId { get; set; }
    public Trainer? Trainer { get; set; }
    public int CourseId { get; set; }
    public Course? Course { get; set; }

    // UpdatedAt
    // CreatedAt
    // Status
}
