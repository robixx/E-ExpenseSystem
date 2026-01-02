using Expense.Application.ModelViews;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expense.Application.Interface
{
    public interface IAuth
    {
        Task<(string Message, bool Status, UserDto list)> LoginAsync(LoginViewModel model);
    }
}
