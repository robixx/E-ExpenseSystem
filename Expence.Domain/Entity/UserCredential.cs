using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Expense.Domain.Entity
{
    public class UserCredential
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; } 
        public string? LoginName { get; set; }
        public string? Password { get; set; }
    }
}
