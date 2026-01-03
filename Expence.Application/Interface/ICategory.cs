using Expense.Application.ModelViews;
using Expense.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expense.Application.Interface
{
    public interface ICategory
    {
        Task<(string Message, bool Status)> AddCategoryAsync(CategoryDto category);
        Task<(string Message, bool Status, List<CategoryDto>datalist)> CategoryListAsync(string UserId);
    }
}
