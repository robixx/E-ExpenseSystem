using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Expense.Application.ModelViews
{
    public class InComeDto
    {
        public int IncomeId { get; set; }      
        public string IncomeName { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public int IsActive { get; set; }
        public int CreatedBy { get; set; }        
        public DateTime MonthOfIncome { get; set; }
    }
}
