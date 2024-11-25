using Developers.Models;
using Developers.Persistence;
using Developers.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Developers.Repositories.Implementations;

public class InscripcionesRepository : RepositoryBase<Inscripciones>, IInscripcionesRepository
{
    private readonly DevelopersDbContext _db;
    public InscripcionesRepository(DevelopersDbContext db) : base(db)
    {
        _db = db;
    }

    public void Actualizar(Inscripciones inscripciones)
    {
        var inscripcionesDb = _db.Inscripcion.FirstOrDefault(t => t.InscripcionesId== inscripciones.InscripcionesId);

        if (inscripcionesDb is not null)
        {
            inscripcionesDb.StudentId = inscripciones.StudentId;
            inscripcionesDb.CourseId = inscripciones.CourseId;
            inscripcionesDb.HoursInscripciones = inscripciones.HoursInscripciones;
            inscripcionesDb.DetailsInscripciones = inscripciones.DetailsInscripciones;
            inscripcionesDb.SessionDateInscripciones = inscripciones.SessionDateInscripciones;
            inscripcionesDb.UpdatedAt = DateTime.Now;
            inscripcionesDb.Status = inscripciones.Status;

            _db.SaveChanges();
        }
    }

    public IEnumerable<SelectListItem> ObtenerTodosDropdownLista(string obj)
    {
        if (obj == "Student")
            return _db.Students.Where(t => t.Status == true).Select(t => new SelectListItem
            {
                Text = t.FirstName + " " + t.LastName,
                Value = t.StudentId.ToString()
            });

        if (obj == "Course")
            return _db.Courses.Where(t => t.Status == true).Select(t => new SelectListItem
            {
                Text = t.Name,
                Value = t.CourseId.ToString()
            });
        return null;
    }
}
