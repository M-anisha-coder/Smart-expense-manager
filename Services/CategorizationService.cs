using SmartExpenseManager.Models;

namespace SmartExpenseManager.Services
{
    public class CategorizationService
    {
        private readonly Dictionary<string, string> _keywordCategories =
        new(StringComparer.OrdinalIgnoreCase)
        {
            { "uber", "Transport" },
            { "lyft", "Transport" },
            { "amazon", "Office Supplies" },
            { "starbucks", "Meals" },
            { "stripe", "Income" },
            { "paypal", "Income" },
            { "google", "Software" },
            { "microsoft", "Software" }
        };
        public void CategorizeTransactions(List<Transaction> transactions)
        {
            foreach (var transaction in transactions)
            {
                AssignCategory(transaction);
                DetectBusinessExpense(transaction);
            }
        }
        private void AssignCategory(Transaction transaction)
        {
            if (transaction.Amount > 0)
            {
                transaction.Category = "Income";
                return;
            }

            foreach (var keyword in _keywordCategories.Keys)
            {
                if (transaction.Description.Contains(keyword,
                        StringComparison.OrdinalIgnoreCase))
                {
                    transaction.Category = _keywordCategories[keyword];
                    return;
                }
            }

            transaction.Category = "Uncategorized";
        }

        private void DetectBusinessExpense(Transaction transaction)
        {
            if (transaction.Category == "Software" ||
                transaction.Category == "Office Supplies")
            {
                transaction.IsBusinessExpense = true;
            }
            else
            {
                transaction.IsBusinessExpense = false;
            }
        }

    }
}
