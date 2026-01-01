using Expense.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expense.Application.Interface
{
    public interface ICategory
    {
        Task<(string Message, bool Status)> AddCategoryAsync(Category category);
        Task<(string Message, bool Status, List<Category>datalist)> CategoryListAsync();
    }
}
