using Developers.Models;
using Developers.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;

namespace Developers.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitWork _unitWork;

        public HomeController(ILogger<HomeController> logger, IUnitWork unitWork)
        {
            _logger = logger;
            _unitWork = unitWork;
        }

        public async Task<IActionResult> Index()
        {
            // Obtener todos los cursos, ordenados por la fecha de creación en orden descendente
            var allCourses = await _unitWork.Course.ObtenerTodosAsync(
                orderBy: c => c.OrderByDescending(c => c.CreatedAt),
                isTracking: false);

            // Filtrar los cursos con ID >= 5
            var recentCourses = allCourses.Where(c => c.CourseId >= 5).Take(4).ToList();

            // Obtener cursos adicionales para otras secciones
            var additionalCourses = allCourses.Where(c => c.CourseId < 5).Take(4).ToList();

            // Pasar los cursos filtrados a la vista mediante ViewBag
            ViewBag.RecentCourses = recentCourses;
            ViewBag.AdditionalCourses = additionalCourses;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}