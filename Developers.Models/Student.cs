namespace Developers.Models;

public class Student:EntityBase
{
    public int StudentId { get; set; }
    public string Dni { get; set; }
    public string FirstName { get; set; }
    public string LastName{ get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}
