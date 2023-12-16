using System.Diagnostics;
using New_ConsoleApp.Interfaces;

namespace New_ConsoleApp.Services;

public class FileService : IFileService
{
    private readonly string _filePath;

    //vill inte förenkla för då har jag svårt att förstå vad den gör
    public FileService(string filePath)
    {
        _filePath = filePath;
    }

    //denna metod ska spara till fil
    public bool SaveContentToFile(string content)
    {
        try
        {
            using (var sw = new StreamWriter(_filePath))
            {
                sw.WriteLine(content);
            }
            return true;
        }
        catch (Exception ex)
        {

            Debug.WriteLine(ex.Message);
        }
        return false;
    }

    //denna metod ska hämta från fil
    public string GetContentFromFile()
    {
        try
        {
            if (File.Exists(_filePath))
            {
                using (var sr = new StreamReader(_filePath))
                {
                    return sr.ReadToEnd();
                }
            }
        }
        catch (Exception ex)
        {

            Debug.WriteLine(ex.Message);
        }
        return null!;
    }


}
