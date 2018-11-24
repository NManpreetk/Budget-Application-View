using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BudgetApplicationView.Models
{
    public class CreateTransactionsViewModel
    {
        //public int Id { get; set; }
        [Required]
        public int AccountId { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTimeOffset Date { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public bool IsVoided { get; set; }
        public string EnteredById { get; set; }
    }
}