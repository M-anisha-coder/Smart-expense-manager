namespace SmartExpenseManager.Models;

public class MonthlyReport
{
    public int Year { get; set; }
    public int Month { get; set; }

    public decimal TotalIncome { get; set; }
    public decimal TotalExpenses { get; set; }
    public decimal NetProfit { get; set; }
    public decimal BusinessExpenses { get; set; }

    public List<CategorySummary> CategorySummaries { get; set; }
}
