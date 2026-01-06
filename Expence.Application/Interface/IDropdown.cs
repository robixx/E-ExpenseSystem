using Expense.Application.ModelViews;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expense.Application.Interface
{
    public interface IDropdown
    {
        Task<List<Dropdown>> getCategoryAsync();
        Task<List<Dropdown>> getIncomeAsync();
    }
}
