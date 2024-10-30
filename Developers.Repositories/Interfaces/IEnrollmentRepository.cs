using Developers.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Developers.Repositories.Interfaces;

public interface IEnrollmentRepository : IRepositoryBase<Enrollment>
{
    void Actualizar(Enrollment enrollment);
    IEnumerable<SelectListItem> ObtenerTodosDropdownLista(string obj);
}
