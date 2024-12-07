using System;
using System.IO;
using Newtonsoft.Json.Linq;

public interface IFileReader
{
    string ReadFile(string filePath);
}

public class TxtFileReader : IFileReader
{
    public string ReadFile(string filePath)
    {
        return File.ReadAllText(filePath);
    }
}

public class CsvFileReader : IFileReader
{
    public string ReadFile(string filePath)
    {
        var lines = File.ReadAllLines(filePath);
        return string.Join(Environment.NewLine, lines);
    }
}

public class JsonFileReader : IFileReader
{
    public string ReadFile(string filePath)
    {
        string jsonContent = File.ReadAllText(filePath);
        JObject parsedJson = JObject.Parse(jsonContent);
        return parsedJson.ToString();
    }
}

public class FileReaderAdapter : IFileReader
{
    private readonly IFileReader _fileReader;

    public FileReaderAdapter(IFileReader fileReader)
    {
        _fileReader = fileReader;
    }

    public string ReadFile(string filePath)
    {
        return _fileReader.ReadFile(filePath);
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Створення адаптерів для різних форматів файлів
        IFileReader txtReader = new TxtFileReader();
        IFileReader csvReader = new CsvFileReader();
        IFileReader jsonReader = new JsonFileReader();

        // Використання адаптерів
        FileReaderAdapter txtAdapter = new FileReaderAdapter(txtReader);
        FileReaderAdapter csvAdapter = new FileReaderAdapter(csvReader);
        FileReaderAdapter jsonAdapter = new FileReaderAdapter(jsonReader);

        // Зчитування і виведення вмісту файлів
        Console.WriteLine("TXT File Content: ");
        Console.WriteLine(txtAdapter.ReadFile("example.txt"));

        Console.WriteLine("\nCSV File Content: ");
        Console.WriteLine(csvAdapter.ReadFile("example.csv"));

        Console.WriteLine("\nJSON File Content: ");
        Console.WriteLine(jsonAdapter.ReadFile("example.json"));
    }
}
