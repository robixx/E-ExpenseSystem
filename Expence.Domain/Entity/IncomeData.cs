using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Expense.Domain.Entity
{
    public class IncomeData
    {
        [Key]
        public int IncomeId { get; set; }
        [Required]
        public string IncomeName { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public int IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }= DateTime.Now;
        public DateTime MonthOfIncome { get; set; }
    }
}


