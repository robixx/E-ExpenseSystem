using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Expense.Application.ModelViews
{
    public class UserDto
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;        
        public string UserEmail { get; set; } = string.Empty;
        public string UserPhone { get; set; } = string.Empty;
        public string ImageName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public int IsActive { get; set; }
        public int FamilyMenber { get; set; } = 1;
        public DateTime CreateDate { get; set; } = DateTime.Now;            
        public string? LoginName { get; set; }
        public string? Password { get; set; }
    }
}
