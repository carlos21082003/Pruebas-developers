using Developers.Models;
using Developers.Persistence;
using Developers.Repositories.Interfaces;

namespace Developers.Repositories.Implementations;

public class TrainerRepository : RepositoryBase<Trainer>, ITrainerRepository
{
    private readonly DevelopersDbContext _db;
    public TrainerRepository(DevelopersDbContext db) : base(db)
    {
        _db = db;
    }

    public void Actualizar(Trainer trainer)
    {
        var trainerDB = _db.Trainers.FirstOrDefault(t => t.TrainerId == trainer.TrainerId);

        if (trainerDB is not null)
        {
            // TrainerId no es actualizable
            trainerDB.Dni = trainer.Dni;
            trainerDB.FirstName = trainer.FirstName;
            trainerDB.LastName = trainer.LastName;
            trainerDB.PhoneNumber = trainer.PhoneNumber;
            trainerDB.Email = trainer.Email;
            //trainerDB.CreatedAt = trainer.CreatedAt;
            trainerDB.UpdatedAt = DateTime.Now;
            trainerDB.Status = trainer.Status;

            _db.SaveChanges();
        }
    }
}
