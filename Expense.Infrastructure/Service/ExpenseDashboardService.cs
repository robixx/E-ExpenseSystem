using Expense.Application.Interface;
using Expense.Application.ModelViews;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expense.Infrastructure.Service
{
    public class ExpenseDashboardService : IExpenseDashboardService
    {

        private readonly DatabaseConnection _context;

        public ExpenseDashboardService(DatabaseConnection connection)
        {
            _context = connection;
        }

        public async Task<ExpenseIncomeSummaryVm> GetExpenseIncomeSummaryAsync(int userId)
        {
            try
            {
                var today = DateTime.Today;
                var startOfMonth = new DateTime(today.Year, today.Month, 1);
                var startOfYear = new DateTime(today.Year, 1, 1);

                var dailyExpense = await _context.ExpenseData
                    .Where(x => x.CreatedBy == userId
                             && x.ExpenseDate.Date == today)
                    .SumAsync(x => x.TotalAmount);

                var monthlyExpense = await _context.ExpenseData
                    .Where(x => x.CreatedBy == userId
                             && x.ExpenseDate >= startOfMonth)
                    .SumAsync(x => x.TotalAmount);

                var yearlyExpense = await _context.ExpenseData
                    .Where(x => x.CreatedBy == userId
                             && x.ExpenseDate >= startOfYear)
                    .SumAsync(x => x.TotalAmount);

                var monthlyIncome = await _context.IncomeData
                    .Where(x => x.CreatedBy == userId
                             && x.MonthOfIncome >= startOfMonth)
                    .SumAsync(x => x.TotalAmount);

                var yearlyIncome = await _context.IncomeData
                    .Where(x => x.CreatedBy == userId
                             && x.MonthOfIncome >= startOfYear)
                    .SumAsync(x => x.TotalAmount);

                return new ExpenseIncomeSummaryVm
                {
                    DailyExpense = dailyExpense,
                    MonthlyExpense = monthlyExpense,
                    YearlyExpense = yearlyExpense,

                    MonthlyIncome = monthlyIncome,
                    YearlyIncome = yearlyIncome,

                    MonthlyNetBalance = monthlyIncome - monthlyExpense,
                    YearlyNetBalance = yearlyIncome - yearlyExpense
                };

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
