using Developers.Models;

namespace Developers.Repositories.Interfaces;

public interface ICourseRepository : IRepositoryBase<Course>
{
    void Actualizar(Course course);
}
