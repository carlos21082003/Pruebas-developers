using Developers.Models;
using Developers.Repositories.Interfaces;
using Developers.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Developers.Controllers;

public class TrainersController : Controller
{
    //private readonly DevelopersDbContext _dbContext;
    private readonly IUnitWork _unitWork;
    public TrainersController(
        //DevelopersDbContext context,
        IUnitWork unitWork
        )
    {
        //_dbContext = context;
        _unitWork = unitWork;
    }

    [HttpGet]
    [Authorize(Roles = DS.Role_Admin + "," + DS.Role_Empleado + ","+ DS.Role_Estudiante)]
    public async Task<IActionResult> Index()
    {
        //var trainers = await _dbContext.Trainers.ToListAsync();
        var trainers = await _unitWork.Trainer.ObtenerTodosAsync(
            isTracking: false,
            orderBy: t => t.OrderByDescending(t => t.TrainerId)
           );
        //return Json(new { data = trainers });
        return View(trainers);
    }

    [HttpGet] //Mostrar el formulario de creación
    [Authorize(Roles = DS.Role_Admin + "," + DS.Role_Empleado)]
    public IActionResult Create()
    {
        Trainer trainer = new Trainer();
        trainer.CreatedAt = DateTime.Now;
        trainer.UpdatedAt = DateTime.Now;
        trainer.Status = true;
        return View(trainer);
    }

    [HttpPost] // Recibe datos del formulario y persiste la informacion
    [ValidateAntiForgeryToken]
    [Authorize(Roles = DS.Role_Admin + "," + DS.Role_Empleado)]
    public async Task<IActionResult> Create(Trainer trainer)
    {
        if (ModelState.IsValid)
        {
            trainer.Status = true;
            trainer.CreatedAt = DateTime.Now;
            trainer.UpdatedAt = DateTime.Now;

            //_dbContext.Trainers.Add(trainer);
            //await _dbContext.SaveChangesAsync();
            await _unitWork.Trainer.AgregarAsync(trainer);
            await _unitWork.GuardarAsync();

            return RedirectToAction("Index");
        }
        return View(trainer);
        //return Json(trainer);
    }

    [HttpGet]
    [Authorize(Roles = DS.Role_Admin + "," + DS.Role_Empleado)]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null)
            return NotFound();

        //var trainer = await _dbContext.Trainers.FindAsync(id);
        var trainer = await _unitWork.Trainer.ObtenerAsync(id.GetValueOrDefault());

        if (trainer is null)
            return NotFound();

        return View(trainer);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = DS.Role_Admin + "," + DS.Role_Empleado)]
    public async Task<IActionResult> Edit(Trainer trainer)
    {
        //var dateCreatedDB = await _dbContext.Trainers                            
        //                    .Where(t => t.TrainerId == trainer.TrainerId)
        //                    .AsNoTracking().FirstAsync();
        //return Json(dateCreatedDB);

        if (ModelState.IsValid)
        {
            //trainer.CreatedAt = dateCreatedDB.CreatedAt;
            //trainer.UpdatedAt = DateTime.Now;

            //_dbContext.Trainers.Update(trainer);
            //await _dbContext.SaveChangesAsync();
            _unitWork.Trainer.Actualizar(trainer);
            await _unitWork.GuardarAsync();

            return RedirectToAction("Index");
        }
        return View(trainer);
        //return Json(trainer);
    }

    [HttpGet]
    [Authorize(Roles = DS.Role_Admin)]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null)
            return NotFound();

        //var trainer = await _dbContext.Trainers.FindAsync(id);
        var trainer = await _unitWork.Trainer.ObtenerAsync(id.GetValueOrDefault());

        if (trainer is null)
            return NotFound();

        return View(trainer);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = DS.Role_Admin)]
    public async Task<IActionResult> Delete(Trainer trainer)
    {
        if (trainer is null) return NotFound();
        //_dbContext.Remove(trainer);
        //await _dbContext.SaveChangesAsync();

        _unitWork.Trainer.Remover(trainer);
        await _unitWork.GuardarAsync();

        return RedirectToAction("Index");
    }
}
