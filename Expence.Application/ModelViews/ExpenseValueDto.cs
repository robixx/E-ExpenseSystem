using System;
using System.Collections.Generic;
using System.Text;

namespace Expense.Application.ModelViews
{
    public class ExpenseValueDto
    {
        public int CategoryId { get; set; }       
        public decimal Price { get; set; }       
        public DateTime ExpenseDate { get; set; }
        public string? Description { get; set; }
    }
}
