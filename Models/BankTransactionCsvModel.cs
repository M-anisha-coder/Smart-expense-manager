using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExpenseManager.Models
{
    public class BankTransactionCsvModel
    {
        public string Date { get; set; }
        public string Description { get; set; }
        public string Amount { get; set; }
    }
}
