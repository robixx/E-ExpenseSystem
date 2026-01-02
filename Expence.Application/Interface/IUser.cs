using Expense.Application.ModelViews;
using Expense.Domain.Entity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;


namespace Expense.Application.Interface
{
    public interface IUser
    {
        Task<(string Message, bool Status, List<UserDto>userList)> GetUserAsync();
        Task<(string Message, bool Status)> AddUserAsync(UserDto user, IFormFile image);
    }
}
