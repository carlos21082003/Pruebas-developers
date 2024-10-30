using Developers.Models;
using Developers.Persistence;
using Developers.Repositories.Interfaces;

namespace Developers.Repositories.Implementations;

public class StudentRepository : RepositoryBase<Student>, IStudentRepository
{
    private readonly DevelopersDbContext _db;
    public StudentRepository(DevelopersDbContext db) : base(db)
    {
        _db = db;
    }

    public void Actualizar(Student student)
    {
        var studentDB = _db.Students.FirstOrDefault(s => s.StudentId == student.StudentId);

        if (studentDB is not null)
        {
            // TrainerId no es actualizable
            studentDB.Dni = student.Dni;
            studentDB.FirstName = student.FirstName;
            studentDB.LastName = student.LastName;
            studentDB.PhoneNumber = student.PhoneNumber;
            studentDB.Email = student.Email;
            studentDB.UpdatedAt = DateTime.Now;
            studentDB.Status = student.Status;

            _db.SaveChanges();
        }
    }
}
