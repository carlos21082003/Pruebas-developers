namespace Developers.Repositories.Interfaces;

public interface IUnitWork : IDisposable
{
    // Agregar las Interfaces que se creen
    ITrainerRepository Trainer { get; }
    ICourseRepository Course { get; }
    IClassroomRepository Classroom { get; }
    IStudentRepository Student { get; }
    IEnrollmentRepository Enrollment { get; }
    IApplicationUserRepository ApplicationUser { get; }

    Task GuardarAsync();
}
