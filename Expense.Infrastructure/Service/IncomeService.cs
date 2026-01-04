using Expense.Application.Interface;
using Expense.Application.ModelViews;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expense.Infrastructure.Service
{
    public class IncomeService : IIncome
    {
        public Task<(string Message, bool Status, List<InComeDto> incomelist)> GetIncomeAysnc(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<(string Message, bool Status)> SaveIncomeAysnc(InComeDto income, int userId)
        {
            throw new NotImplementedException();
        }
    }
}
