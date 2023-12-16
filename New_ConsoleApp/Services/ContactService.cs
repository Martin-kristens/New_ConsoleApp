using New_ConsoleApp.Interfaces;
using New_ConsoleApp.Models;
using Newtonsoft.Json;
using System.Diagnostics;

namespace New_ConsoleApp.Services;

public class ContactService : IContactService
{
    //en privat lista av Contact 
    private readonly List<Contact> _contacts = new List<Contact>();
    //kopplar på fileservice för att kunna spara och hämta från fil
    private readonly FileService _fileService = new FileService(@"C:\Education\CSharp\content.json");

    //denna metod sa lägga till en användare i listan och skicka resultatet till SaveContentToFile metoden
    public bool AddContactToList(Contact contact)
    {
        try
        {
            if (!_contacts.Any(x => x.Email == contact.Email))
            {
                _contacts.Add(contact);
                _fileService.SaveContentToFile(JsonConvert.SerializeObject(_contacts));
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }

    //ska hämta lista med användare, listan hämtas från metoden GetContentFromFile
    public IEnumerable<Contact> GetContactsFromList()
    {
        try
        {
            var content = _fileService.GetContentFromFile();
            if (!string.IsNullOrEmpty(content))
            {
                return JsonConvert.DeserializeObject<List<Contact>>(content)!;
            }
            return _contacts.ToList();
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;

    }

    //metoden ska ge detaljerad information av en kontakt baserat på e-post 
    public Contact GetContactDetails(string email)
    {
        try
        {
            Contact foundContact = _contacts.FirstOrDefault(x => x.Email == email)!;
            return foundContact;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    //metoden ska ta bort en kontakt baserat på e-post
    public Contact GetDeleteContact(string email)
    {
        try
        {
            Contact contactToDelete = _contacts.FirstOrDefault(x => x.Email == email)!;
            _contacts.Remove(contactToDelete);

            return contactToDelete;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;

    }

    //metoden ska uppdatera en kontakt baserat på e-post
    public Contact GetUpdateContact(string email)
    {
        try
        {
            Contact contactToUpdate = _contacts.FirstOrDefault(x => x.Email == email)!;

            if (contactToUpdate != null)
            {
                // Uppdatera kontaktinformationen här
                contactToUpdate.FirstName = "UpdatedFirstName";
                contactToUpdate.LastName = "UpdatedLastName";
                contactToUpdate.Phonenumber = "+98765432";
                contactToUpdate.Address = "UpdatedAddress";
                contactToUpdate.City = "UpdatedCity";

                return contactToUpdate;
            }
            //_contacts.Remove(contactToUpdate);

            //return contactToUpdate;


        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }


}
