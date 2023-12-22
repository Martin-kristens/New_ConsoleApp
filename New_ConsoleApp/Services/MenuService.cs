using New_ConsoleApp.Interfaces;
using New_ConsoleApp.Models;

namespace New_ConsoleApp.Services;

/// <summary>
/// Denna klass ansvarar för interaktionen med användaren, den visar menyval, ansvarar för att anvädnaren fyller i rätt när man vill
/// lägga till en kontakt i listan, visa hela listan av kontakter, visa detaljerad info eller ta bort en kontakt.
/// </summary>

public class MenuService : IMenuService
{
    //kopplar samman denna klass med ContactService klassen
    private readonly ContactService _contactService = new ContactService();
    //min huvudmeny 
    public void ShowMainMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("MENU");
            Console.WriteLine();
            Console.WriteLine($"{"1.",-4} Add Contact");
            Console.WriteLine($"{"2.",-4} View All Contacts");
            Console.WriteLine($"{"3.",-4} View Contact detail");
            Console.WriteLine($"{"4.",-4} Delete Contact");
            Console.WriteLine($"{"0.",-4} Exit Application");

            Console.Write("Enter your option: ");

            var option = Console.ReadLine() ?? "";

            switch (option)
            {
                case "1":
                    ShowAddContactOption();
                    break;
                case "2":
                    ShowAllContacts();
                    break;
                case "3":
                    ShowContactDetails();
                    break;
                case "4":
                    ShowDeleteOption();
                    break; ;
                case "0":
                    ShowExitOption();
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
            Console.ReadKey();
        }
    }

    //denna metod tar hand om avslut av programmet då användaren trycker på 0
    private void ShowExitOption()
    {
        Console.Clear();
        Console.Write("Are you sure you want to exit the application? (y/n): ");

        //finns det inget i option så skicka en tom sträng
        var option = Console.ReadLine() ?? "";

        if (option.Equals("y", StringComparison.CurrentCultureIgnoreCase))
        {
            Environment.Exit(0);
        }
        else
        {
            Console.WriteLine("Press any key to return to menu");
        }

    }

    //metdoden tar in data från användaren och skickar datan till ContactService för att se om det inte redan 
    //finns en kontakt med samma e-post
    //ContactService skickar sedan tillbaka resultatet hit
    private void ShowAddContactOption()
    {
        var contact = new Contact();

        Console.Clear();

        Console.Write("First Name: ");
        contact.FirstName = Console.ReadLine()!;

        Console.Write("Last Name: ");
        contact.LastName = Console.ReadLine()!;

        Console.Write("Phone Number: ");
        contact.Phonenumber = Console.ReadLine()!;

        Console.Write("Email: ");
        contact.Email = Console.ReadLine()!;

        Console.Write("Address: ");
        contact.Address = Console.ReadLine()!;

        Console.Write("City: ");
        contact.City = Console.ReadLine()!;

        var addedContact = _contactService.AddContactToList(contact);

        if (addedContact)
        {
            Console.WriteLine("Contact has been added");
        }
        else
        {
            Console.WriteLine("The email was already existing");
        }
    }

    //denna metod ansvarar för att visa en lista med användare
    private void ShowAllContacts()
    {
        var allContacts = _contactService.GetContactsFromList();

        if (allContacts.Any())
        {
            Console.WriteLine("CONTACTS");
            Console.WriteLine();
            foreach (var contact in allContacts)
            {
                Console.WriteLine($"{contact.FirstName} {contact.LastName} {contact.Email}");
            }
        }
        else
        {
            Console.WriteLine("The list was empty");
        }
    }

    //denna metod ansvarar för att visa en detaljerad kontakt
    private void ShowContactDetails()
    {
        Console.Clear();

        Console.Write("Type in the email of the contact you would like to view: ");
        string inputEmail = Console.ReadLine()!;
        Console.Clear();

        Contact contact = new Contact();
        contact.Email = inputEmail;

        var findContactInList = _contactService.GetContactDetails(inputEmail);

        if (findContactInList != null)
        {
            Console.WriteLine($"CONTACT:\n\n {findContactInList.FirstName} {findContactInList.LastName} {findContactInList.Phonenumber} {findContactInList.Email} " +
                $" {findContactInList.Address} {findContactInList.City}");
        }
        else
        {
            Console.WriteLine("No email could be found that matches the one you typed in");
        }
    }

    //denna metod ansvarar för att ta emot data från användaren som sedan skickas vidare till ContactService 
    //klassen som utför en kontroll om det finns en kontakt eller  inte baserat på e-post 
    //finns det en kontakt så tas kontakten bort från listan och skickar tillbaka resultate hit som sedan skriver 
    //ut ett meddelande om det gick bra eller inte
    private void ShowDeleteOption()
    {
        Console.Clear();

        Console.Write("Type in the email of the contact you would like to delete: ");
        var contactToDelete = Console.ReadLine()!;

        Console.Clear();

        Contact contact = new Contact();
        contact.Email = contactToDelete;

        var deletedContact = _contactService.GetDeleteContact(contactToDelete);

        if (deletedContact != null)
        {
            Console.WriteLine($"{deletedContact.Email} has been deleted");
        }
        else
        {
            Console.WriteLine("No email could be found that matches the one you typed in");
        }
    }
}
