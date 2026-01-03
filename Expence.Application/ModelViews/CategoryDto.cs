using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Expense.Application.ModelViews
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        
        public string CategoryName { get; set; } = string.Empty;
        public string? CategoryDescription { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public int CreateBy { get; set; }
        public bool IsActive { get; set; }
    }
}
