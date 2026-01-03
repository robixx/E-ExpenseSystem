using System;
using System.Collections.Generic;
using System.Text;

namespace Expense.Application.ModelViews
{
    public class ExpenseDataDto
    {
        public int ExpenseId { get; set; }
        public int CategoryId { get; set; }       
        public decimal Quantity { get; set; } = 0;
        public decimal Price { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime ExpenseDate { get; set; }        
        public string? Description { get; set; }
        public string? CategoryName { get; set; }
       
    }
}
