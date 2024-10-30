using Developers.Models;

namespace Developers.Repositories.Interfaces;

public interface IStudentRepository : IRepositoryBase<Student>
{
    void Actualizar(Student student);
}
