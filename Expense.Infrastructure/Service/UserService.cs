using Expense.Application.Interface;
using Expense.Application.ModelViews;
using Expense.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expense.Infrastructure.Service
{
    public class UserService : IUser
    {
        private readonly DatabaseConnection _connection;
        private readonly string _imagePath;

        public UserService(DatabaseConnection connection, IConfiguration configuration)
        {
            _connection = connection;
            _imagePath = configuration["ImageStorage:TokenImagePath"]
                     ?? throw new ArgumentNullException("ImageStorage:TokenImagePath not configured");
        }

        public Task<(string Message, bool Status)> AddUserAsync(UserDto user)
        {
            throw new NotImplementedException();
        }

        public async Task<(string Message, bool Status, List<UserDto> userList)> GetUserAsync()
        {
            try
            {
                List<UserDto> dtos = new List<UserDto>();
                var list = await (from a in _connection.Users
                                  join b in _connection.UserCredential on a.UserId equals b.UserId
                                  where a.IsActive == 1
                                  select new UserDto
                                  {
                                      UserId = a.UserId,
                                      UserName = a.UserName,
                                      UserEmail = a.UserEmail,
                                      UserPhone = a.UserPhone,
                                      ImageName = a.ImageName,
                                      IsActive = a.IsActive,
                                      FamilyMenber = a.FamilyMenber,
                                      CreateDate = a.CreateDate,
                                      Id = b.Id,
                                      LoginName = b.LoginName,
                                      Password = b.Password,
                                  }).ToListAsync();


                return ("Data Retrieved Succeesfully", true, dtos);

            }
            catch (Exception ex)
            {
                return ($"Error:{ex.Message}", false, new List<UserDto>());
            }
        }
    }
}
