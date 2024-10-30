using Developers.Persistence;
using Developers.Repositories.Interfaces;

namespace Developers.Repositories.Implementations;

public class UnitWork : IUnitWork
{
    private readonly DevelopersDbContext _db;
    public ITrainerRepository Trainer { get; private set; }
    public ICourseRepository Course { get; private set; }
    public IClassroomRepository Classroom { get; private set; }
    public IStudentRepository Student { get; private set; }
    public IEnrollmentRepository Enrollment { get; private set; }
    public IApplicationUserRepository ApplicationUser { get; private set; }
    public UnitWork(DevelopersDbContext db)
    {
        _db = db;
        Trainer = new TrainerRepository(_db);
        Course = new CourseRepository(_db);
        Classroom = new ClassroomRepository(_db);
        Student = new StudentRepository(_db);
        Enrollment = new EnrollmentRepository(_db);
        ApplicationUser = new ApplicationUserRepository(_db);
    }
    public void Dispose()
    {
        _db.Dispose();
    }

    public async Task GuardarAsync()
    {
        await _db.SaveChangesAsync();
    }
}
