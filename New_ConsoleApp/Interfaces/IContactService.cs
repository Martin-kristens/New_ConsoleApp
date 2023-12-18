using New_ConsoleApp.Models;

namespace New_ConsoleApp.Interfaces
{
    public interface IContactService
    {
        bool AddContactToList(Contact contact);
        Contact GetContactDetails(string email);
        IEnumerable<Contact> GetContactsFromList();
        Contact GetDeleteContact(string email);
        //Contact GetUpdateContact(string email);
    }
}