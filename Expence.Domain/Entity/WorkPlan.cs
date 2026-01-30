using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expense.Domain.Entity
{
    public  class WorkPlan
    {
        [Key]
        public int Id { get; set; }
        public string ValueType { get; set; } = string.Empty;
        public int CreatedBy { get; set; }
        public int IsTrue { get; set; }
        public DateTime CreateDate { get; set; }
        
    }
}
