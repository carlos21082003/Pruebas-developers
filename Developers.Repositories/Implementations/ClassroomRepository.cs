using Developers.Models;
using Developers.Persistence;
using Developers.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Developers.Repositories.Implementations;

public class ClassroomRepository : RepositoryBase<Classroom>, IClassroomRepository
{
    private readonly DevelopersDbContext _db;
    public ClassroomRepository(DevelopersDbContext db) : base(db)
    {
        _db = db;
    }

    public void Actualizar(Classroom classroom)
    {
        var classroomDB = _db.Classrooms.FirstOrDefault(t => t.ClassroomId == classroom.ClassroomId);

        if (classroomDB is not null)
        {
            classroomDB.TrainerId = classroom.TrainerId;
            classroomDB.CourseId = classroom.CourseId;
            classroomDB.Hours = classroom.Hours;
            classroomDB.Details = classroom.Details;
            classroomDB.SessionDate = classroom.SessionDate;
            classroomDB.UpdatedAt = DateTime.Now;
            classroomDB.Status = classroom.Status;

            _db.SaveChanges();
        }
    }

    public IEnumerable<SelectListItem> ObtenerTodosDropdownLista(string obj)
    {
        if (obj == "Trainer")
            return _db.Trainers.Where(t => t.Status == true).Select( t => new SelectListItem
            {
                Text = t.FirstName + " " + t.LastName,
                Value = t.TrainerId.ToString()
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
