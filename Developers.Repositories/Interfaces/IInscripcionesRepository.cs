using Developers.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Developers.Repositories.Interfaces;

public interface IInscripcionesRepository : IRepositoryBase<Inscripciones>
{
    void Actualizar(Inscripciones inscripciones);
    IEnumerable<SelectListItem> ObtenerTodosDropdownLista(string obj);
}
