using Developers.Models;
using Developers.Persistence;
using Developers.Repositories.Interfaces;

namespace Developers.Repositories.Implementations;

public class CourseRepository : RepositoryBase<Course>, ICourseRepository
{
    private readonly DevelopersDbContext _db;
    public CourseRepository(DevelopersDbContext db) : base(db)
    {
        _db = db;
    }

    public void Actualizar(Course course)
    {
        var courseDB = _db.Courses.FirstOrDefault(t => t.CourseId == course.CourseId);

        if (courseDB is not null)
        {
            courseDB.Name = course.Name;
            courseDB.Description = course.Description;
            courseDB.Hours = course.Hours;
            courseDB.UpdatedAt = DateTime.Now;
            courseDB.Status = course.Status;
            courseDB.Theme = course.Theme;
            courseDB.Price = course.Price;
        }
    }
}
