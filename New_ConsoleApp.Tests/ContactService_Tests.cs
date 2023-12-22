using New_ConsoleApp.Models;
using New_ConsoleApp.Services;
using System.Collections.Generic;


namespace New_ConsoleApp.Tests;

public class ContactService_Tests
{
    [Fact]
    //kollar om metoden kan lägga till en användare till listan
    public void AddToListShould_AddOneContactToContactList_ThenReturnTrue()
    {
        // Arrange
        Contact contact = new Contact { FirstName = "Martin", LastName = "Kristensen", Email = "martin@mail.se" };
      
        ContactService contactService = new ContactService();

        // Act

        bool result = contactService.AddContactToList(contact);

        // Assert
        Assert.True(result);
    }

    [Fact]
    //kollar om metoden kan hämta och visa en lista med användare
    public void GetAllFromListShould_GetAllContactsInContactList_ThenReturnListOfContacts()
    {
        // Arrannge
        ContactService contactService = new ContactService();
        Contact contact = new Contact 
        { 
            FirstName = "Martin", 
            LastName = "Kristensen", 
            Email = "martin@mail.se", 
            Phonenumber = "+12345456", 
            Address = "Rallarvägen", 
            City = "L-A"
        };
        contactService.AddContactToList(contact);

        // Act
        var result = contactService.GetContactsFromList();

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Any());
        Assert.IsAssignableFrom<IEnumerable<Contact>>(result);
    }

    [Fact]
    //kollar om metoden kan visa detaljerad information om en användare 
    public void GetContactDetailsFromListShould_GetContactDetailInformationFromContactList_ThenReturnDetailOfContact()
    {
        // Arrange
        ContactService contactService = new ContactService();
        Contact contact = new Contact
        {
            FirstName = "Martin",
            LastName = "Kristensen",
            Email = "martin@mail.se",
            Phonenumber = "+12345456",
            Address = "Rallarvägen",
            City = "L-A"
        };
        contactService.AddContactToList(contact);

        // Act
        Contact result = contactService.GetContactDetails("martin@mail.se");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(contact, result);
    }

    [Fact]
    //kollar om metoden tar bort en kontakt 
    public void GetDeleteContactShould_DeleteOneContactFromList_ThenReturnUpdatedList()
    {
        // Arrange
        ContactService contactService = new ContactService();
        Contact contact = new Contact
        {
            FirstName = "Martin",
            LastName = "Kristensen",
            Email = "martin@mail.se"
        };
        contactService.AddContactToList(contact);

        // Act

        Contact deletedContact = contactService.GetDeleteContact("martin@mail.se");

        // Assert
        Assert.NotNull(deletedContact);
        Assert.Equal(contact, deletedContact);

        // Check if the contact is removed from the list
        Contact result = contactService.GetContactDetails("martin@mail.se");
        Assert.Null(result);
    }
}
