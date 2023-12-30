using New_ConsoleApp.Interfaces;

namespace New_ConsoleApp.Models;

//egenskaper för min kontakt
public class Contact : IContact
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Phonenumber { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string City { get; set; } = null!;
}
