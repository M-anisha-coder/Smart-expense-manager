using SmartExpenseManager.Models;
using System.Text.Json;

namespace SmartExpenseManager.Services;

public class StorageService
{
    private readonly string _dataFolder = "data";
    private readonly string _filePath;

    public StorageService()
    {
        var envPath = Environment.GetEnvironmentVariable("SMART_EXPENSE");

        if (!string.IsNullOrWhiteSpace(envPath))
        {
            if (Directory.Exists(envPath))
            {
                _filePath = Path.Combine(envPath, "transactions.json");
            }
            else
            {
                _filePath = envPath;
            }
        }
        else
        {
            var defaultDirectory = Path.Combine(Directory.GetCurrentDirectory(), "data");
            _filePath = Path.Combine(defaultDirectory, "transactions.json");
        }

        EnsureDataFolderExists();
    }

    private void EnsureDataFolderExists()
    {
        var directory = Path.GetDirectoryName(_filePath);

        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
    }


    public void SaveTransactions(List<Transaction> transactions)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        var json = JsonSerializer.Serialize(transactions, options);
        File.WriteAllText(_filePath, json);
    }

    public List<Transaction> LoadTransactions()
    {
        if (!File.Exists(_filePath))
            return new List<Transaction>();

        var json = File.ReadAllText(_filePath);

        return JsonSerializer.Deserialize<List<Transaction>>(json)
               ?? new List<Transaction>();
    }

    public void AppendTransactions(List<Transaction> newTransactions)
    {
        var existingTransactions = LoadTransactions();

        // Basic duplicate prevention
        var combined = existingTransactions
            .Concat(newTransactions)
            .GroupBy(t => new { t.Date, t.Description, t.Amount })
            .Select(g => g.First())
            .ToList();

        SaveTransactions(combined);
    }
}
