using System.ComponentModel.DataAnnotations;

namespace Developers.Models;

public class Trainer : EntityBase
{
    [Key]
    public int TrainerId { get; set; } // PK: Id, Model+ID
    [Required(ErrorMessage = "El DNI es requerido")]
    [MaxLength(15, ErrorMessage = "El DNI no debe exceder los 15 dígitos"),
        MinLength(8, ErrorMessage = "El DNI debe tener mínimo 8 caracteres")]
    public string Dni { get; set; }
    public string FirstName { get; set; } // Nombres
    [MaxLength(250)] // Fluent API tiene prioridad
    public string LastName { get; set; } // Apellidos
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    //public DateTime CreatedAt { get; set; }
    //public DateTime UpdatedAt { get; set; }
    //public bool Status { get; set; } // Activo (0) , Inactivo (1)
}
