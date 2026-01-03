using Expense.Application.Interface;
using Expense.Application.ModelViews;
using Expense.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expense.Infrastructure.Service
{
    public class ExpenseService : IExpense
    {

        private readonly DatabaseConnection _connection;

        public ExpenseService(DatabaseConnection connection)
        {
            _connection = connection;
        }

        public async Task<(string Message, bool Status)> ExpenseAysnc(ExpenseValueDto model)
        {
            try
            {
                if (model != null)
                {
                    var item = new ExpenseData
                    {
                        CategoryId = model.CategoryId,
                        UserId = 2,
                        Price = model.Price,
                        TotalAmount = model.Price,
                        ExpenseDate = model.ExpenseDate,
                        CreateDate = DateTime.Now,
                        Description = model.Description,
                        IsDeleted = false,
                    };

                    await _connection.ExpenseData.AddAsync(item);
                    await _connection.SaveChangesAsync();
                }
                return ("Expense Save Successfuly !", true);
            }
            catch (Exception ex)
            {
                return (ex.Message, false);
            }
        }
    }
}
