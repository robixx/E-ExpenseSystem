using Expense.Application.Interface;
using Expense.Application.ModelViews;
using Expense.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expense.Infrastructure.Service
{
    public class IncomeService : IIncome
    {

        private readonly DatabaseConnection _connection;

        public IncomeService(DatabaseConnection connection)
        {
            _connection = connection;
        }

        public async Task<(string Message, bool Status, List<InComeDto> incomelist)> GetIncomeAysnc(int userId)
        {
            try
            {
                List<InComeDto> list = new List<InComeDto>();

                list = await _connection.IncomeData
                       .Where(i => i.CreatedBy == userId)
                       .Select(i => new InComeDto
                       {
                           IncomeId = i.IncomeId,
                           IncomeName = i.IncomeName,
                           CreatedBy = i.CreatedBy,
                           MonthOfIncome = i.MonthOfIncome,
                           TotalAmount = i.TotalAmount,
                           IsActive = i.IsActive,
                       }).ToListAsync();

                return ("Data Retrived Successfully", true, list);

            }
            catch (Exception ex)
            {
                return ($"Error:{ex.Message}", false, new List<InComeDto>());
            }
        }

        public async Task<(string Message, bool Status)> SaveIncomeAysnc(InComeDto income)
        {
            try
            {
                var result = new IncomeData
                {
                    IncomeName = income.IncomeName,
                    TotalAmount = income.TotalAmount,
                    IsActive = 1,
                    CreateDate = DateTime.Now,
                    CreatedBy = income.CreatedBy,
                    MonthOfIncome = income.MonthOfIncome,
                };
                 await _connection.IncomeData.AddRangeAsync(result);
                await _connection.SaveChangesAsync();

                return ("Data Added Successfully", true);

            }catch (Exception ex)
            {
                return ($"Error : {ex.Message}", false);
            }
        }
    }
}
