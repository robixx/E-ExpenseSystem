using Expense.Application.ModelViews;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expense.Application.Interface
{
    public interface IExpense
    {
        Task<(string Message, bool Status)> ExpenseAysnc(ExpenseValueDto model);
    }
}
