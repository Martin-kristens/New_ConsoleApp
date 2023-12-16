namespace New_ConsoleApp.Interfaces
{
    public interface IContact
    {
        string Address { get; set; }
        string City { get; set; }
        string Email { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Phonenumber { get; set; }
    }
}