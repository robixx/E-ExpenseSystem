using Expense.Application.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expense.Application.Interface
{
    public interface IWorkData
    {
        Task<List<WorkPlanDto>> getWorkPlan(int CreatedBy);
        Task<(string Message, bool Status)> SaveWorkPlan(WorkPlanDto work);
    }
}
