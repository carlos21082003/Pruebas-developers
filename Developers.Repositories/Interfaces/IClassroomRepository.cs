using Developers.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Developers.Repositories.Interfaces;

public interface IClassroomRepository : IRepositoryBase<Classroom>
{
    void Actualizar(Classroom classroom);
    IEnumerable<SelectListItem> ObtenerTodosDropdownLista(string obj);
}
