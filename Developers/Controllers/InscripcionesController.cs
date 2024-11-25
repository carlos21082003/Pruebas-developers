using Developers.Models;
using Developers.Models.ViewModels;
using Developers.Repositories.Implementations;
using Developers.Repositories.Interfaces;
using Developers.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Developers.Controllers;

public class InscripcionesController : Controller
{
    private readonly IUnitWork _unitWork;
    [BindProperty]
    public InscripcionesVM inscripcionesVM { get; set; }
    public InscripcionesController(IUnitWork unitWork)
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
        InscripcionesVM inscripcionesVM = new InscripcionesVM()
        {
            Inscripciones = new Models.Inscripciones() { SessionDateInscripciones = DateTime.Now },
            CourseList = _unitWork.Inscripciones.ObtenerTodosDropdownLista("Course"),
            StudentList = _unitWork.Inscripciones.ObtenerTodosDropdownLista("Student")
        };

        return View(inscripcionesVM);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(InscripcionesVM inscripcionesVM)
    {
        if (inscripcionesVM is null) return NotFound();
        if (ModelState.IsValid)
        {
            await _unitWork.Inscripciones.AgregarAsync(inscripcionesVM.Inscripciones);
            await _unitWork.GuardarAsync();
            TempData[DS.Successfull] = "Sesión creada correctamente.";
            return RedirectToAction("Index"); // Redirigir a la vista de índice
        }
        inscripcionesVM.CourseList = _unitWork.Inscripciones.ObtenerTodosDropdownLista("Course");
        inscripcionesVM.StudentList = _unitWork.Inscripciones.ObtenerTodosDropdownLista("Student");
        TempData[DS.Error] = "Error al guardar la sesión, intente de nuevo.";
        return View(inscripcionesVM);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int? id)
    {
        if (id is null) return NotFound();
        inscripcionesVM = new InscripcionesVM();
        inscripcionesVM.Inscripciones = await _unitWork.Inscripciones.ObtenerPrimeroAsync(filter: c => c.InscripcionesId == id, includeProperties: "Student,Course");
        inscripcionesVM.Enrollments = await _unitWork.Enrollment.ObtenerTodosAsync(filter: e => e.InscripcionesId == id, includeProperties: "Student");

        return View(inscripcionesVM);
    }

    [HttpPost]
    public async Task<IActionResult> Details(int inscripcionesId, int studentId)
    {
        inscripcionesVM = new InscripcionesVM();
        inscripcionesVM.Inscripciones = await _unitWork.Inscripciones.ObtenerPrimeroAsync(filter: c => c.InscripcionesId == inscripcionesId);
        var enrollment = await _unitWork.Enrollment.ObtenerPrimeroAsync(e => e.InscripcionesId == inscripcionesId && e.StudentId == studentId);

        // Si el estudiante está agregado, retornar un mensaje
        if (enrollment is null)
        {
            inscripcionesVM.Enrollment = new Models.Enrollment();
            inscripcionesVM.Enrollment.StudentId = studentId;
            inscripcionesVM.Enrollment.InscripcionesId = inscripcionesId;

            await _unitWork.Enrollment.AgregarAsync(inscripcionesVM.Enrollment);
            await _unitWork.GuardarAsync();
            TempData[DS.Successfull] = "Participante agregado correctamente";
        }
        else
        {
            TempData[DS.Error] = "Error al agregar el participante, intente de nuevo";
        }

        return RedirectToAction("DetailsInscripciones", new { id = inscripcionesId });
    }

    /// <summary>
    /// Buscar participante segun valor ingresado
    /// </summary>
    /// <param name="term"></param>
    /// <returns></returns>

    [HttpGet]
    public async Task<IActionResult> SearchStudent(string term)
    {
        if (!string.IsNullOrEmpty(term))
        {
            var productsList = await _unitWork.Student.ObtenerTodosAsync(p => p.Status == true);
            var data = productsList.Where(x => x.Dni.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                                            x.FirstName.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                                            x.LastName.Contains(term, StringComparison.OrdinalIgnoreCase)).ToList();
            return Json(data);
        }
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null) return NotFound();
        var enrollment = await _unitWork.Enrollment.ObtenerPrimeroAsync(e => e.EnrollmentId == id);
        if (enrollment == null)
        {
            return NotFound();
        }
        return View(enrollment);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("EnrollmentId,InscripcionesId,StudentId,PreTest,PostTest,Inscripciones,Student")] Enrollment enrollment)
    {
        if (id != enrollment.EnrollmentId)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            if (enrollment.PostTest < 0 || enrollment.PostTest > 20 || enrollment.PreTest < 0 || enrollment.PreTest > 20)
            {
                TempData["Error"] = "Error , menor a 0 o mayor a 20";
                return View(enrollment);
            }
            try
            {
                if (enrollment.PostTest >= 12)
                {
                    enrollment.Passed = true;
                }
                else
                {
                    enrollment.Passed = false;
                }

                _unitWork.Enrollment.Actualizar(enrollment);
                await _unitWork.GuardarAsync();
                TempData["Successful"] = "Participante actualizado correctamente";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnrollmentExists(enrollment.EnrollmentId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        if (!ModelState.IsValid)
        {
            var errors = ModelState.SelectMany(x => x.Value.Errors.Select(z => z.ErrorMessage));
            // Haz algo con los errores aquí, como imprimirlos en la consola.
            foreach (var error in errors)
            {
                Console.WriteLine(error);
            }
        }


        return View(enrollment);
    }

    private bool EnrollmentExists(int id)
    {
        return _unitWork.Enrollment.ObtenerPrimeroAsync(e => e.EnrollmentId == id) != null;
    }

    // Acción para eliminar un participante
    [HttpPost]
    public async Task<IActionResult> DeleteParticipant(int id)
    {
        var enrollment = await _unitWork.Enrollment.ObtenerPrimeroAsync(e => e.EnrollmentId == id);
        if (enrollment == null)
        {
            return NotFound();
        }

        int inscripcionesId = enrollment.InscripcionesId;
        _unitWork.Enrollment.Remover(enrollment);
        await _unitWork.GuardarAsync();

        return RedirectToAction("DetailsInscripciones", new { id = inscripcionesId });
    }


    #region API para Javascript
    /// <summary>
    /// Listar todas las sesiones registradas
    /// </summary>
    /// <returns>Json</returns>
    [HttpGet]
    public async Task<IActionResult> ListarTodos()
    {
        var inscripciones = await _unitWork.Inscripciones.ObtenerTodosAsync(
            includeProperties: "Student,Course",
            orderBy: c => c.OrderByDescending(c => c.InscripcionesId),
            isTracking: false);

        return Json(new { data = inscripciones });
    }
    #endregion
}
