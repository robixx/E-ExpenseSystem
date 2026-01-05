using System;
using System.Collections.Generic;
using System.Text;

namespace Expense.Application.Interface
{
    public interface IActivityLogService
    {
        Task LogAsync(string action, string module, string description);
    }
}
