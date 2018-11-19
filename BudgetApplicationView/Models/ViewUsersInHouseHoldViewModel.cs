using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BudgetApplicationView.Models
{
    public class ViewUsersInHouseHoldViewModel
    {
        public int Id { get; set; }
        public IList<string> Email { get; set; }
    }
}