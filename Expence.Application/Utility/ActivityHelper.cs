using System;
using System.Collections.Generic;
using System.Text;

namespace Expense.Application.Utility
{
    public static class ActivityHelper
    {
        public static string GetBadgeClass(string timeAgo)
        {
            if (timeAgo.Contains("min")) return "text-success";   // green
            if (timeAgo.Contains("hrs")) return "text-danger";      // red
            if (timeAgo.Contains("day")) return "text-primary";      // blue
            if (timeAgo.Contains("week")) return "text-muted";       // gray

            return "text-secondary"; // fallback
        }
    }
}
