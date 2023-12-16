using New_ConsoleApp.Models;
using New_ConsoleApp.Services;


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
        IEnumerable<Contact> result = contactService.GetContactsFromList();

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Any());
    }

    [Fact]
    //kollar om metoden kan visa detaljerad information om en användare 
    public void GetContactDetailsFromListShould_GetContactDetailInformationFromContactList_ThenReturnDetailOfContact()
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
        Contact result = contactService.GetContactDetails("martin@mail.se");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Martin", result.FirstName);
        Assert.Equal("Kristensen", result.LastName);
        Assert.Equal("martin@mail.se", result.Email);
        Assert.Equal("+12345456", result.Phonenumber);
        Assert.Equal("Rallarvägen", result.Address);
        Assert.Equal("L-A", result.City);
    }

    [Fact]
    //kollar om metoden uppdaterar en kontakt
    public void GetContactUpdateFromListShould_GetContactUpdateInformationFromContactList_ThenReturnUpdatedInformationOfContact()
    {
        // Arrannge
        ContactService contactService = new ContactService();
        Contact originalContact = new Contact
        {
            FirstName = "Martin",
            LastName = "Kristensen",
            Email = "martin@mail.se",
            Phonenumber = "+12345456",
            Address = "Rallarvägen",
            City = "L-A"
        };
        contactService.AddContactToList(originalContact);


        // Act
        Contact updatedContact = new Contact
        {
            FirstName = "UpdatedFirstName",
            LastName = "UpdatedLastName",
            Email = "martin@mail.se",
            Phonenumber = "+98765432",
            Address = "UpdatedAddress",
            City = "UpdatedCity"
        };
        var result = contactService.GetUpdateContact("martin@mail.se");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("UpdatedFirstName", result.FirstName);
        Assert.Equal("UpdatedLastName", result.LastName);
        Assert.Equal("martin@mail.se", result.Email);
        Assert.Equal("+98765432", result.Phonenumber);
        Assert.Equal("UpdatedAddress", result.Address);
        Assert.Equal("UpdatedCity", result.City);

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
        Assert.Equal("Martin", deletedContact.FirstName);
        Assert.Equal("Kristensen", deletedContact.LastName);
        Assert.Equal("martin@mail.se", deletedContact.Email);

        // Check if the contact is removed from the list
        Contact result = contactService.GetContactDetails("martin@mail.se");
        Assert.Null(result);
    }
}
