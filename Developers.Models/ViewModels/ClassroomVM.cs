using Microsoft.AspNetCore.Mvc.Rendering;

namespace Developers.Models.ViewModels;

public class ClassroomVM
{
    public Classroom Classroom { get; set; }
    public Enrollment? Enrollment { get; set; }
    public IEnumerable<Enrollment>? Enrollments { get; set; }
    public IEnumerable<SelectListItem>? CourseList { get; set; }
    public IEnumerable<SelectListItem>? TrainerList { get; set; }
}
