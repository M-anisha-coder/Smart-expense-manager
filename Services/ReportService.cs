using SmartExpenseManager.Models;

namespace SmartExpenseManager.Services;

public class ReportService
{
    public MonthlyReport GenerateMonthlyReport(
        List<Transaction> transactions,
        int year,
        int month)
    {
        var monthlyTransactions = transactions
            .Where(t => t.Date.Year == year && t.Date.Month == month)
            .ToList();

        var totalIncome = monthlyTransactions
            .Where(t => t.Amount > 0)
            .Sum(t => t.Amount);

        var totalExpenses = monthlyTransactions
            .Where(t => t.Amount < 0)
            .Sum(t => Math.Abs(t.Amount));

        var businessExpenses = monthlyTransactions
            .Where(t => t.IsBusinessExpense)
            .Sum(t => Math.Abs(t.Amount));

        var categoryBreakdown = monthlyTransactions
            .Where(t => t.Amount < 0)
            .GroupBy(t => t.Category)
            .Select(g => new CategorySummary
            {
                Category = g.Key,
                TotalAmount = g.Sum(t => Math.Abs(t.Amount))
            })
            .OrderByDescending(c => c.TotalAmount)
            .ToList();

        return new MonthlyReport
        {
            Year = year,
            Month = month,
            TotalIncome = totalIncome,
            TotalExpenses = totalExpenses,
            NetProfit = totalIncome - totalExpenses,
            BusinessExpenses = businessExpenses,
            CategorySummaries = categoryBreakdown
        };
    }

    public MonthlyReport GenerateYearlyReport(
    List<Transaction> transactions,
    int year)
    {
        var yearlyTransactions = transactions
            .Where(t => t.Date.Year == year)
            .ToList();

        var totalIncome = yearlyTransactions
            .Where(t => t.Amount > 0)
            .Sum(t => t.Amount);

        var totalExpenses = yearlyTransactions
            .Where(t => t.Amount < 0)
            .Sum(t => Math.Abs(t.Amount));

        var businessExpenses = yearlyTransactions
            .Where(t => t.IsBusinessExpense)
            .Sum(t => Math.Abs(t.Amount));

        var categoryBreakdown = yearlyTransactions
            .Where(t => t.Amount < 0)
            .GroupBy(t => t.Category)
            .Select(g => new CategorySummary
            {
                Category = g.Key,
                TotalAmount = g.Sum(t => Math.Abs(t.Amount))
            })
            .OrderByDescending(c => c.TotalAmount)
            .ToList();

        return new MonthlyReport
        {
            Year = year,
            Month = 0,
            TotalIncome = totalIncome,
            TotalExpenses = totalExpenses,
            NetProfit = totalIncome - totalExpenses,
            BusinessExpenses = businessExpenses,
            CategorySummaries = categoryBreakdown
        };
    }

}
