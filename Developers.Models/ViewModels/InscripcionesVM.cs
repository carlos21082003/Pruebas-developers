using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Developers.Models.ViewModels;

public class InscripcionesVM
{
    public Inscripciones Inscripciones { get; set; }
    public Enrollment? Enrollment { get; set; }
    public IEnumerable<Enrollment>? Enrollments { get; set; }
    public IEnumerable<SelectListItem>? CourseList { get; set; }
    public IEnumerable<SelectListItem>? StudentList { get; set; }
}
