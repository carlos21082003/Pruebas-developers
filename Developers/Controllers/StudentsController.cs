using Developers.Models;
using Developers.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Developers.Controllers;

public class StudentsController : Controller
{
    private readonly IUnitWork _unitWork;
    public StudentsController(IUnitWork unitWork)
    {
        _unitWork = unitWork;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Create()
    {
        Student student = new Student();
        return View(student);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Student student)
    {
        if (student == null) { return NotFound(); }

        if (ModelState.IsValid)
        {
            await _unitWork.Student.AgregarAsync(student);
            await _unitWork.GuardarAsync();

            TempData["Successful"] = "Estudiante creado correctamente";

            return RedirectToAction("Index");
        }

        return View(student);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null) return NotFound();

        Student student = new Student();
        student = await _unitWork.Student.ObtenerAsync(id.GetValueOrDefault());

        if (student is null) return NotFound();

        return View(student);

    }

    [HttpPost]
    public async Task<IActionResult> Edit(Student student)
    {
        if (ModelState.IsValid)
        {
            _unitWork.Student.Actualizar(student);
            await _unitWork.GuardarAsync();

            TempData["Successful"] = "Estudiante actualizado correctamente";

            return RedirectToAction("Index");
        }

        return View(student);
    }

    #region API
    /// <summary>
    /// Listar todos los estudiantes registrados
    /// </summary>
    /// <returns>Json</returns>
    [HttpGet]
    public async Task<IActionResult> ListarTodos()
    {
        var students = await _unitWork.Student.ObtenerTodosAsync(
            orderBy: c => c.OrderByDescending(c => c.StudentId),
            isTracking: false);

        return Json(new { data = students });
    }

    /// <summary>
    /// Eliminar un registro enviado por Ajax
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Json</returns>
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var studentDB = await _unitWork.Student.ObtenerAsync(id);

        if (studentDB is null) // Si es nulo muestre un mensaje
            return Json(new { success = false, message = "Error al eliminar el estudiante" });

        // Si eliminó correctamente muestre mensaje
        _unitWork.Student.Remover(studentDB);
        await _unitWork.GuardarAsync();

        return Json(new { success = true, message = "Estudiante eliminado correctamente" });
    }
    #endregion
}
