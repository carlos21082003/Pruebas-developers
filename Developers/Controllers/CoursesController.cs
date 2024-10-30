using Developers.Models;
using Developers.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Developers.Controllers;

public class CoursesController : Controller
{
    private readonly IUnitWork _unitWork;
    public CoursesController(IUnitWork unitWork)
    {
        _unitWork = unitWork;
    }

    //[HttpGet]
    //public async Task<IActionResult> Index()
    //{
    //    var courses = await _unitWork.Course.ObtenerTodosAsync(
    //        orderBy: c => c.OrderByDescending(c => c.CourseId),
    //        isTracking: false);

    //    return View(courses);
    //}

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Create()
    {
        Course course = new Course();
        return View(course);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Course course)
    {
        if (course == null) { return NotFound(); }

        if (ModelState.IsValid)
        {
            course.CreatedAt = DateTime.Now;
            course.UpdatedAt = DateTime.Now;
            course.Status = true;

            await _unitWork.Course.AgregarAsync(course);
            await _unitWork.GuardarAsync();

            TempData["Successful"] = "Curso creado correctamente";

            return RedirectToAction("Index");
        }

        return View(course);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null) return NotFound();

        Course course = new Course();
        course = await _unitWork.Course.ObtenerAsync(id.GetValueOrDefault());

        if (course is null) return NotFound();

        return View(course);

    }

    [HttpPost]
    public async Task<IActionResult> Edit(Course course)
    {
        if (ModelState.IsValid)
        {
            _unitWork.Course.Actualizar(course);
            await _unitWork.GuardarAsync();

            TempData["Successful"] = "Curso actualizado correctamente";

            return RedirectToAction("Index");
        }

        return View(course);
    }
    [HttpGet]
    public IActionResult AddCourses()
    {
        return View(); // Mostrará la vista para agregar un curso
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddCourses(Course course)
    {
        if (ModelState.IsValid)
        {
            // Asignar fechas de creación y actualización
            course.CreatedAt = DateTime.Now;
            course.UpdatedAt = DateTime.Now;
            course.Status = true; // Marcamos el curso como activo por defecto

            // Agregar curso a la base de datos
            await _unitWork.Course.AgregarAsync(course);
            await _unitWork.GuardarAsync();

            // Almacenar un mensaje de éxito en TempData
            TempData["Success"] = "El curso ha sido agregado exitosamente.";
            return RedirectToAction("Index");

            // Redirigir a la lista de cursos
            return RedirectToAction("Index");
        }

        return View(course);
    }

    #region API
    /// <summary>
    /// Listar todos los cursos registrados
    /// </summary>
    /// <returns>Json</returns>
    [HttpGet]
    public async Task<IActionResult> ListarTodos()
    {
        var courses = await _unitWork.Course.ObtenerTodosAsync(
            orderBy: c => c.OrderByDescending(c => c.CourseId),
            isTracking: false);

        return Json(new { data = courses });
    }

    /// <summary>
    /// Eliminar un registro enviado por Ajax
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Json</returns>
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var courseDB = await _unitWork.Course.ObtenerAsync(id);

        if (courseDB is null) // Si es nulo muestre un mensaje
            return Json(new { success = false, message = "Error al eliminar el curso" });

        // Si eliminó correctamente muestre mensaje
        _unitWork.Course.Remover(courseDB);
        await _unitWork.GuardarAsync();

        return Json(new { success = true, message = "Curso eliminado correctamente" });
    }
    #endregion

    #region Detalles del curso
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var course = await _unitWork.Course.ObtenerAsync(id);
        if (course == null)
        {
            return NotFound();
        }

        return View(course);
    }
    #endregion

   



}
