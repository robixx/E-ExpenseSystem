using Expense.Application.ModelViews;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expense.Application.Interface
{
    public interface IExpenseDashboardService
    {
        Task<ExpenseIncomeSummaryVm> GetExpenseIncomeSummaryAsync(int userId);
        Task<List<ActivityLogDto>> GetActivityLogSummaryAsync();
    }
}
