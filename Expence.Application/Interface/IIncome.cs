using Expense.Application.ModelViews;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expense.Application.Interface
{
    public interface IIncome
    {
        Task<(string Message, bool Status, List<InComeDto> incomelist)> GetIncomeAysnc(int userId);
        Task<(string Message, bool Status)> SaveIncomeAysnc(InComeDto income);
        Task<(string Message, bool Status)> BackIncomeAysnc(BackupMoneyDto model);
        Task<(string Message, bool Status,List<BackupMoneyDto>list)> GetBackIncomeAysnc(int userId);
    }
}
