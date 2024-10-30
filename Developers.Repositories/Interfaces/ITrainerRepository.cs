using Developers.Models;

namespace Developers.Repositories.Interfaces;

public interface ITrainerRepository : IRepositoryBase<Trainer>
{
    void Actualizar(Trainer trainer);
}
