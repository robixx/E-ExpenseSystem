using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Expense.Domain.Entity
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string UserEmail { get; set; }= string.Empty;
        public string UserPhone { get; set; }=string.Empty;
        public string ImageName {  get; set; }=string.Empty;
        public string Gender {  get; set; }=string.Empty;
        public int IsActive { get; set; }
        public int FamilyMenber { get; set; }
        public DateTime CreateDate { get; set; }= DateTime.Now;  
        public DateTime LastLogin { get; set; }  

    }
}
