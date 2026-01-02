using Expense.Application.Interface;
using Expense.Application.ModelViews;
using Expense.Domain.Entity;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
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


        public async Task<(string Message, bool Status)> AddUserAsync(UserDto user, IFormFile image)
        {
            try
            {
                if (image != null && image.Length > 0)
                {
                    var folder = Path.Combine(_imagePath);
                    if (!Directory.Exists(folder))
                        Directory.CreateDirectory(folder);

                    var fileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
                    var filePath = Path.Combine(folder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }

                    user.ImageName = fileName;
                }
                if (user != null)
                {
                    var list = new User
                    {
                        UserName = user.UserName,
                        UserEmail = user.UserEmail,
                        UserPhone = user.UserPhone,
                        ImageName=user.ImageName,
                        Gender = user.Gender,
                        IsActive=Convert.ToInt32(user.IsActive),
                        FamilyMenber = user.FamilyMenber,
                        CreateDate= DateTime.Now,   

                    };
                    await _connection.Users.AddAsync(list);
                    await _connection.SaveChangesAsync();
                    if (!string.IsNullOrEmpty(user.LoginName) && !string.IsNullOrEmpty(user.Password))
                    {
                        var cre = new UserCredential
                        {
                            UserId = list.UserId,
                            LoginName= user.LoginName,
                            Password= HashPassword( user.Password)
                        };

                        await _connection.UserCredential.AddAsync(cre);
                        await _connection.SaveChangesAsync();
                    }
                    
                }

                return ($"{user?.UserName} Create Successfully", true);
               
             }
            catch (Exception ex)
            {
                return ($"Error:{ex.Message}", false);
            }
        }

        private string HashPassword(string password)
        {
            // Generate a 128-bit salt using a secure PRNG
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Derive a 256-bit subkey (use HMACSHA256 with 10000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            // Store salt + hash together (Base64)
            return $"{Convert.ToBase64String(salt)}:{hashed}";
        }



        public async Task<(string Message, bool Status, List<UserDto> userList)> GetUserAsync()
        {
            try
            {
                List<UserDto> dtos = new List<UserDto>();
                 dtos = await (from a in _connection.Users
                                  join b in _connection.UserCredential on a.UserId equals b.UserId
                                  where a.IsActive == 1
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
