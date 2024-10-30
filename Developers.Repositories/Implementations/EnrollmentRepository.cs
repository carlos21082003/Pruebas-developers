using Developers.Models;
using Developers.Persistence;
using Developers.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Developers.Repositories.Implementations;

public class EnrollmentRepository : RepositoryBase<Enrollment>, IEnrollmentRepository
{
    private readonly DevelopersDbContext _db;
    public EnrollmentRepository(DevelopersDbContext db) : base(db)
    {
        _db = db;
    }

    public void Actualizar(Enrollment enrollment)
    {
        var enrollmentDB = _db.Enrollments.FirstOrDefault(t => t.EnrollmentId == enrollment.EnrollmentId);

        if (enrollmentDB is not null)
        {
            enrollmentDB.StudentId = enrollment.StudentId;
            enrollmentDB.ClassroomId = enrollment.ClassroomId;
            enrollmentDB.PreTest = enrollment.PreTest;
            enrollmentDB.PostTest = enrollment.PostTest;
            enrollmentDB.Passed = enrollment.Passed;
            enrollmentDB.UpdatedAt = DateTime.Now;
            enrollmentDB.Status = enrollment.Status;

            _db.SaveChanges();
        }
    }

    public IEnumerable<SelectListItem> ObtenerTodosDropdownLista(string obj)
    {        
        return null;
    }
}
