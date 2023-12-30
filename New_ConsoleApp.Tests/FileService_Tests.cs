using New_ConsoleApp.Services;
using Newtonsoft.Json;

namespace New_ConsoleApp.Tests;

public class FileService_Tests
{
    [Fact]
    // Integrationstest
    //kollar om metoden kan hämta data från fil
    public void GetContentFromFileShould_ReturnCorrectContent_IfFileExists()
    {
        // Arrange
        string filePath = @"C:\Education\CSharp\content.json";
        //här används dynamic för att skapa ett anonymt objekt med förväntade egenskaper 
        dynamic expectedContent = new { FirstName = "Martin", LastName = "Kristensen" };
        FileService fileService = new FileService(filePath);

        // Först måste man skapa filen och skriva innehållet i den
        fileService.SaveContentToFile(JsonConvert.SerializeObject(expectedContent));

        // Act
        string resultContent = fileService.GetContentFromFile();

        // Assert
        Assert.NotNull(resultContent);
        Assert.True(resultContent.Contains(JsonConvert.SerializeObject(expectedContent)));
    }

    [Fact]

    //integrationstest
    //kollar om metoden kan spara till fil
    public void SaveToFileShould_ReturnFalse_IfFilePathDoNotExists()
    {
        // Arrange
        string filePath = @$"C:\{Guid.NewGuid()}\content.json";
        FileService fileService = new FileService(filePath);
        string content = "Test content";

        // Act
        bool result = fileService.SaveContentToFile(content);

        // Assert
        Assert.False(result);
    }
}
