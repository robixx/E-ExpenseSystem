using Expense.Application.Interface;
using Expense.Domain.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Expense.Infrastructure.Service
{
    public class ActivityLogService : IActivityLogService
    {
        private readonly DatabaseConnection _connection;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ActivityLogService(DatabaseConnection connection, IHttpContextAccessor httpContextAccessor)
        {
            _connection = connection;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task LogAsync(string action, string module, string description)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var userClaims = httpContext?.User;

            // ✅ Get UserId from Claims
            var userIdClaim = userClaims?.FindFirst(ClaimTypes.NameIdentifier);
            int userId = userIdClaim != null ? Convert.ToInt32(userIdClaim.Value) : 0;

            // ✅ Get UserName
            var userName = userClaims?.Identity?.Name ?? "System";

            var log = new ActivityLog
            {
                UserId = userId,
                UserName = userName,
                Action = action,
                Module = module,
                Description = description,
                CreatedAt = DateTime.Now
            };

            await _connection.ActivityLog.AddAsync(log);
            await _connection.SaveChangesAsync();
        }
    }
}
