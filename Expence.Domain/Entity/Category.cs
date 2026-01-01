using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Expense.Domain.Entity
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; } = string.Empty;
        public string CategoryDescription { get; set; }= string.Empty;
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public int CreateBy { get; set; }
        public int IsActive { get; set; }
        
    }
}
