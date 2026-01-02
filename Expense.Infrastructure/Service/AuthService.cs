using Expense.Application.Interface;
using Expense.Application.ModelViews;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Expense.Infrastructure.Service
{
    public class AuthService : IAuth
    {
        private readonly DatabaseConnection _connection;
       

        public AuthService(DatabaseConnection connection)
        {
            _connection = connection;
          
        }

        public async Task<(string Message, bool Status, UserDto list)> LoginAsync(LoginViewModel model)
        {
            try
            {
                if (model == null)
                    return ("Invalid input", false, new UserDto());


                var userCredential = await _connection.UserCredential
                     .FirstOrDefaultAsync(u => u.LoginName == model.LoginName);

                if (userCredential == null)
                    return ("Username or password is incorrect", false, new UserDto());

                // Verify password using stored hash
                if (!VerifyPassword(model.Password.Trim(), userCredential.Password))
                    return ("Username or password is incorrect", false, new UserDto());


                // Get user details
                var user = await (from a in _connection.Users
                                  join b in _connection.UserCredential on a.UserId equals b.UserId
                                  where a.IsActive == 1 && a.UserId == userCredential.UserId
                                  select new UserDto
                                  {
                                      UserId = a.UserId,
                                      UserName = a.UserName,
                                      UserEmail = a.UserEmail,
                                      UserPhone = a.UserPhone,
                                      ImageName = a.ImageName,
                                      IsActive = a.IsActive.ToString(),
                                      FamilyMenber = a.FamilyMenber,
                                      CreateDate = a.CreateDate,
                                      Id = b.Id,
                                      LoginName = b.LoginName,                                     
                                  }).FirstOrDefaultAsync();

                return ("Login Successful", true, user ?? new UserDto());
            
                

            }catch (Exception ex)
            {
                return ($"In valid UserName and Password {ex.Message}", false, new UserDto());
            }
        }



        private bool VerifyPassword(string enteredPassword, string storedPassword)
        {
            var parts = storedPassword.Split(':');
            if (parts.Length != 2) return false;

            var salt = Convert.FromBase64String(parts[0]);
            var storedHash = parts[1];

            var hashOfInput = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: enteredPassword,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashOfInput == storedHash;
        }
    }
}
