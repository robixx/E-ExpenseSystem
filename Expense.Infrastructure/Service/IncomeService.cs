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

        public async Task<(string Message, bool Status)> BackIncomeAysnc(BackupMoneyDto model)
        {
            try
            {
                var result = new BackupMoney
                {
                    PurposeName = model.PurposeName,
                    Amount = model.Amount,
                    IsActive = 1,
                    CreatedAt = DateTime.Now,
                    CreatedBy = model.CreatedBy,
                    MonthofDate = model.MonthofDate,
                    IncomeId = model.IncomeId,
                };
                await _connection.BackupMoney.AddRangeAsync(result);
                await _connection.SaveChangesAsync();

                return ("Data Added Successfully", true);


            }
            catch (Exception ex)
            {
                return ($"Error: {ex.Message}", false);
            }
        }

        public async Task<(string Message, bool Status, List<BackupMoneyDto> list)> GetBackIncomeAysnc(int userId)
        {
            try
            {
                List<BackupMoneyDto> list = new List<BackupMoneyDto>();

                list = await _connection.BackupMoney
                       .Where(i => i.CreatedBy == userId)
                       .Select(i => new BackupMoneyDto
                       {
                           IncomeId = i.IncomeId,
                           PurposeName = i.PurposeName,
                           CreatedBy = i.CreatedBy,
                           MonthofDate = i.MonthofDate,
                           Amount = i.Amount,
                           IsActive = i.IsActive,
                       }).ToListAsync();

                return ("Data Retrived Successfully", true, list);

            }
            catch (Exception ex)
            {
                return ($"Error:{ex.Message}", false, new List<BackupMoneyDto>());
            }
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
