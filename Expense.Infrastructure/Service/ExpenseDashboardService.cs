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

        public async Task<List<ActivityLogDto>> GetActivityLogSummaryAsync()
        {
            try
            {


                var list = await _context.ActivityLog
                    .OrderByDescending(x => x.CreatedAt)
                    .Take(10)
                    .Select(i => new ActivityLogDto
                    {
                        Id = i.Id,
                        UserId = i.UserId,
                        UserName = i.UserName,
                        Description = i.Description,
                        CreatedAt = i.CreatedAt
                        // DO NOT COMPUTE TIMEAGO HERE
                    })
                    .ToListAsync();

                // Compute TimeAgo in memory
                list.ForEach(x => x.TimeAgo = GetTimeAgo(x.CreatedAt));
                return list;


            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        private string GetTimeAgo(DateTime dateTime)
        {
            var timeSpan = DateTime.Now - dateTime;

            if (timeSpan.TotalSeconds < 60)
                return "just now";

            if (timeSpan.TotalMinutes < 60)
                return $"{(int)timeSpan.TotalMinutes} min ago";

            if (timeSpan.TotalHours < 24)
                return $"{(int)timeSpan.TotalHours} hrs";

            if (timeSpan.TotalDays < 7)
                return $"{(int)timeSpan.TotalDays} day";

            if (timeSpan.TotalDays < 30)
                return $"{(int)(timeSpan.TotalDays / 7)} week";

            return dateTime.ToString("dd MMM yyyy");
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
                var saveIncome = await _context.BackupMoney
                    .Where(x => x.CreatedBy == userId)
                    .SumAsync(x => x.Amount);

                var netYearlyIncome = (await (
                                 from income in _context.IncomeData
                                 where income.CreatedBy == userId
                                    && income.MonthOfIncome >= startOfYear
                                 join backup in _context.BackupMoney
                                     on income.IncomeId equals backup.IncomeId into backupGroup
                                 select new
                                 {
                                     IncomeTotal = income.TotalAmount,
                                     BackupSum = backupGroup.Sum(b => (decimal?)b.Amount) ?? 0
                                 }
                             ).ToListAsync())
                             .Sum(x => x.IncomeTotal - x.BackupSum);

                return new ExpenseIncomeSummaryVm
                {
                    DailyExpense = dailyExpense,
                    MonthlyExpense = monthlyExpense,
                    YearlyExpense = yearlyExpense,

                    MonthlyIncome = monthlyIncome,
                    YearlyIncome = netYearlyIncome,

                    SaveNetBalance = saveIncome,
                    YearlyNetBalance = netYearlyIncome - yearlyExpense
                };

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
