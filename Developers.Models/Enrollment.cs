namespace Developers.Models;

public class Enrollment:EntityBase
{
    public int EnrollmentId { get; set; }
    public int ClassroomId { get; set; }
    public Classroom? Classroom { get; set; }
    public int StudentId { get; set; }
    public Student? Student { get; set; }
    public decimal? PreTest { get; set; }
    public decimal? PostTest { get; set; }
    public bool Passed { get; set; }
}
