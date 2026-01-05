using System;
using System.Collections.Generic;
using System.Text;

namespace Expense.Domain.Entity
{
    public class ActivityLog
    {
        public int Id { get; set; }

        public int UserId { get; set; } 
        public string UserName { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;     

        public string Module { get; set; } = string.Empty;       

        public string Description { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
