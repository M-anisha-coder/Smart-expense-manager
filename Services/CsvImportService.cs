using CsvHelper;
using CsvHelper.Configuration;
using SmartExpenseManager.Models;
using System.Globalization;

namespace SmartExpenseManager.Services
{
    public class CsvImportService
    {
       
        public List<Transaction> Import(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("CSV file not found.", filePath);

            var transactions = new List<Transaction>();

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                TrimOptions = TrimOptions.Trim,
                IgnoreBlankLines = true,
            };

            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, config);

            var records = csv.GetRecords<BankTransactionCsvModel>();

            foreach (var record in records)
            {
                if (TryConvertToTransaction(record, out var transaction))
                {
                    transactions.Add(transaction);
                }
            }

            return transactions;
        }

        private bool TryConvertToTransaction(BankTransactionCsvModel record, out Transaction transaction)
        {
            transaction = null;

            try
            {
                if (!DateTime.TryParse(record.Date, out var date))
                    return false;

                if (!decimal.TryParse(
                        record.Amount.Replace("$", "").Replace(",", ""),
                        NumberStyles.Any,
                        CultureInfo.InvariantCulture,
                        out var amount))
                    return false;

                transaction = new Transaction
                {
                    Date = date,
                    Description = record.Description?.Trim(),
                    Amount = amount,
                    Category = "Uncategorized",
                    IsBusinessExpense = false
                };

                return true;
            }
            catch
            {
                return false;
            }
        }
    }

}

