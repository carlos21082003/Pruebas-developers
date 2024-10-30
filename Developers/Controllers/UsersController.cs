using Developers.Persistence;
using Developers.Repositories.Interfaces;
using Developers.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Developers.Controllers;

[Authorize(Roles = DS.Role_Admin)]
public class UsersController : Controller
{
    private readonly IUnitWork _unitWork;
    private readonly DevelopersDbContext _context;
    public UsersController(IUnitWork unitWork, DevelopersDbContext context)
    {
        _unitWork = unitWork;
        _context = context;
    }
    public IActionResult Index()
    {
        return View();
    }

    #region API para JAVASCRIPT
    /// <summary>
    /// Lista todos los usuarios existentes
    /// </summary>
    /// <returns>Json</returns>
    [HttpGet]
    public async Task<IActionResult> ListarTodos()
    {
        // Tdos los usuarios
        //var users = await _unitWork.ApplicationUser.ObtenerTodosAsync();

        // Todos los usuarios, excepto el que tiene la sesión activa  
        var claimIdentity = (ClaimsIdentity)this.User.Identity; // Usuario logueado
        var actualUser = claimIdentity.FindFirst(ClaimTypes.NameIdentifier); // Name usuario logueado (correo)
        var users = await _unitWork.ApplicationUser.ObtenerTodosAsync(filter: u => u.Id != actualUser.Value);

        // Los roles
        var userRoles = await _context.UserRoles.ToListAsync();
        var roles = await _context.Roles.ToListAsync();

        // Llenar la propiedad Role del Modelo ApplicationUser
        foreach (var user in users) {
            var roleId = userRoles.FirstOrDefault(u => u.UserId == user.Id).RoleId;
            user.Role = roles.FirstOrDefault(r => r.Id == roleId).Name;
        }
        
        return Json(new { data = users });
    }
    #endregion
}
