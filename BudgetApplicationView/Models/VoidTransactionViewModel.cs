using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BudgetApplicationView.Models
{
    public class VoidTransactionViewModel
    {
        public int Id { get; set; }
        public bool IsVoided { get; set; }
    }
}