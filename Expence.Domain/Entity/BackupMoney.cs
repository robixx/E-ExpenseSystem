using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Expense.Domain.Entity
{
    public class BackupMoney
    {
        [Key]
        public int BackupId { get; set; }
        public int IncomeId { get; set; }
        public string PurposeName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime MonthofDate { get; set; }
        public DateTime CreatedAt { get; set; }= DateTime.Now;
        public int CreatedBy { get; set; }
        public int IsActive { get; set; }
        
    }
}
