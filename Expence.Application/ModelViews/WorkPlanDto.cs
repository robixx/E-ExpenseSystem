using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expense.Application.ModelViews
{
    public class WorkPlanDto
    {
        public int Id { get; set; }
        public string ValueType { get; set; } = string.Empty;
        public int CreatedBy { get; set; }
        public int IsTrue { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
