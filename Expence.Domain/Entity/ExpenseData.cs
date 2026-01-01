using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Expense.Domain.Entity
{
    public class ExpenseData
    {
        [Key]
        public int ExpenseId { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public decimal Quantity { get; set; } = 0;
        public decimal Price { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime ExpenseDate { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;      
        public string? Description { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
