using Expense.Application.Interface;
using Expense.Application.ModelViews;
using Expense.Domain.Entity;
using Microsoft.EntityFrameworkCore;
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

        public async Task<(string Message, bool Status)> ExpenseAysnc(ExpenseValueDto model, int userId)
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
                        CreatedBy=userId,
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

        public async Task<(string Message, bool Status, List<ExpenseDataDto> list)> GetExpenseAysnc(int userId)
        {
            try
            {
                var exlist = await (
                             from b in _connection.ExpenseData
                             join c in _connection.Category
                                 on b.CategoryId equals c.CategoryId
                             where b.IsDeleted == false 
                             select new ExpenseDataDto
                             {
                                 ExpenseId = b.ExpenseId,
                                 CategoryId = b.CategoryId,
                                 Price = b.Price,
                                 TotalAmount = b.TotalAmount,
                                 ExpenseDate = b.ExpenseDate,
                                 Description = b.Description,
                                 CategoryName = c.CategoryName, 
                                 CreateBy=b.CreatedBy
                             }
                         ).Where(i=>i.CreateBy==userId).ToListAsync();

                return ("Data Retrived Successfuly", true, exlist);
            }
            catch (Exception ex)
            {
                return (ex.Message, false, new List<ExpenseDataDto>());
            }
        }
    }
}
