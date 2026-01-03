using Expense.Application.Interface;
using Expense.Application.ModelViews;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expense.Infrastructure.Service
{
    public class DropdownService : IDropdown
    {
        private readonly DatabaseConnection _connection;
        public DropdownService(DatabaseConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<Dropdown>> getCategoryAsync()
        {
            try
            {
                var category = await _connection.Category.Where(i => i.IsActive == 1)
                    .Select(c => new Dropdown
                    {
                        Id=c.CategoryId,
                        Name=c.CategoryName
                    }).ToListAsync();

                return category;

            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
