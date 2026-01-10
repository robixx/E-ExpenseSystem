using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Expense.Application.ModelViews
{
    public class TodosDto
    {
        
        public int TodoId { get; set; }        
        public int UserId { get; set; }
        public string Title { get; set; } = string.Empty;        
        public string? Description { get; set; }

        public bool IsCompleted { get; set; } = false;
        
        public int Priority { get; set; } = 2;

        public DateTime? DueDate { get; set; }

        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }
    }
}
