using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Expense.Domain.Entity
{
    public class Todos
    {
        [Key]
        public int TodoId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        public bool IsCompleted { get; set; } = false;

        /// <summary>
        /// 1 = Low, 2 = Medium, 3 = High
        /// </summary>
        public int Priority { get; set; } = 2;

        public DateTime? DueDate { get; set; }

        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }
    }
}
