using Expense.Application.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expense.Infrastructure.Service
{
    public class ActivityLogFilter : IAsyncActionFilter
    {
        private readonly IActivityLogService _activityLogService;

        public ActivityLogFilter(IActivityLogService activityLogService)
        {
            _activityLogService = activityLogService;
        }

        public async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            var result = await next();

            if (result.Exception != null)
                return;

            // ✅ Only POST requests
            if (context.HttpContext.Request.Method != HttpMethods.Post)
                return;

            // ✅ Only successful actions (redirect usually means saved)
            if (result.Result is RedirectToActionResult)
            {
                var controller = context.RouteData.Values["controller"]?.ToString();
                var action = context.RouteData.Values["action"]?.ToString();

                await _activityLogService.LogAsync(
                    action: $"{action}",
                    module: controller ?? "",
                    description: $"{action} successfully"
                );
            }
        }
    }
}
