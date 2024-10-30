using Developers.Models;
using Developers.Models.ViewModels;
using Developers.Repositories.Interfaces;
using Developers.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Developers.Controllers;

public class ClassroomsController : Controller
{
    private readonly IUnitWork _unitWork;
    [BindProperty]
    public ClassroomVM classroomVM { get; set; }
    public ClassroomsController(IUnitWork unitWork)
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
        ClassroomVM classroomVM = new ClassroomVM()
        {
            Classroom = new Models.Classroom(){ SessionDate= DateTime.Now },            
            CourseList = _unitWork.Classroom.ObtenerTodosDropdownLista("Course"),
            TrainerList = _unitWork.Classroom.ObtenerTodosDropdownLista("Trainer")
        };

        return View(classroomVM);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ClassroomVM classroomVM)
    {
        if (classroomVM is null) return NotFound();
        if (ModelState.IsValid)
        {
            await _unitWork.Classroom.AgregarAsync(classroomVM.Classroom);
            await _unitWork.GuardarAsync();
            TempData[DS.Successfull] = "Sesión creada correctamente.";
            return RedirectToAction("Index");
        }
        classroomVM.CourseList = _unitWork.Classroom.ObtenerTodosDropdownLista("Course");
        classroomVM.TrainerList = _unitWork.Classroom.ObtenerTodosDropdownLista("Trainer");
        TempData[DS.Error] = "Error al guardar la sesión, intente de nuevo.";
        return View(classroomVM);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int? id)
    {
        if(id is null) return NotFound();
        classroomVM = new ClassroomVM();
        classroomVM.Classroom = await _unitWork.Classroom.ObtenerPrimeroAsync(filter: c => c.ClassroomId == id, includeProperties:"Trainer,Course");
        classroomVM.Enrollments = await _unitWork.Enrollment.ObtenerTodosAsync(filter: e => e.ClassroomId == id, includeProperties:"Student");

        return View(classroomVM);
    }

    [HttpPost]
    public async Task<IActionResult> Details(int classroomId, int studentId)
    {
        classroomVM = new ClassroomVM();
        classroomVM.Classroom = await _unitWork.Classroom.ObtenerPrimeroAsync(filter: c => c.ClassroomId == classroomId) ;
        var enrollment = await _unitWork.Enrollment.ObtenerPrimeroAsync(e => e.ClassroomId == classroomId && e.StudentId == studentId);

        // Si el estudiante está agregado, retornar un mensaje
        if(enrollment is null)
        {
            classroomVM.Enrollment = new Models.Enrollment();
            classroomVM.Enrollment.StudentId = studentId;
            classroomVM.Enrollment.ClassroomId = classroomId;

            await _unitWork.Enrollment.AgregarAsync(classroomVM.Enrollment);
            await _unitWork.GuardarAsync();
            TempData[DS.Successfull] = "Participante agregado correctamente";
        }
        else
        {
            TempData[DS.Error] ="Error al agregar el participante, intente de nuevo";
        }

        return RedirectToAction("Details", new { id = classroomId});
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
    public async Task<IActionResult> Edit(int id, [Bind("EnrollmentId,ClassroomId,StudentId,PreTest,PostTest,Classroom,Student")] Enrollment enrollment)
    {
        if (id != enrollment.EnrollmentId)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            if(enrollment.PostTest <0 || enrollment.PostTest > 20 || enrollment.PreTest<0 || enrollment.PreTest >20)
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

        int classroomId = enrollment.ClassroomId;
        _unitWork.Enrollment.Remover(enrollment);
        await _unitWork.GuardarAsync();

        return RedirectToAction("Details", new { id = classroomId });
    }


    #region API para Javascript
    /// <summary>
    /// Listar todas las sesiones registradas
    /// </summary>
    /// <returns>Json</returns>
    [HttpGet]
    public async Task<IActionResult> ListarTodos()
    {
        var classrooms = await _unitWork.Classroom.ObtenerTodosAsync(
            includeProperties: "Trainer,Course",
            orderBy: c => c.OrderByDescending(c => c.ClassroomId),
            isTracking: false);

        return Json(new { data = classrooms });
    }
    #endregion
}
