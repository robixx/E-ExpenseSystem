using System;
using System.Collections.Generic;
using System.Text;

namespace Expense.Application.ModelViews
{
    public class ExpenseIncomeSummaryVm
    {
        public decimal DailyExpense { get; set; }
        public decimal MonthlyExpense { get; set; }
        public decimal YearlyExpense { get; set; }

        public decimal MonthlyIncome { get; set; }
        public decimal YearlyIncome { get; set; }

        public decimal SaveNetBalance { get; set; }
        public decimal YearlyNetBalance { get; set; }
    }
}
