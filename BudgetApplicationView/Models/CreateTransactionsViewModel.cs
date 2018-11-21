using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BudgetApplicationView.Models
{
    public class CreateTransactionsViewModel
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Description { get; set; }
        public DateTimeOffset Date { get; set; }
        public decimal Amount { get; set; }
        public int CategoryId { get; set; }
        public bool IsVoided { get; set; }
        public string EnteredById { get; set; }
    }
}