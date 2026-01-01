using Expense.Application.Interface;
using Expense.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expense.Infrastructure.Service
{
    public class CategoryService : ICategory
    {
        public Task<(string Message, bool Status)> AddCategoryAsync(Category category)
        {
            throw new NotImplementedException();
        }

        public Task<(string Message, bool Status, List<Category> datalist)> CategoryListAsync()
        {
            throw new NotImplementedException();
        }
    }
}
