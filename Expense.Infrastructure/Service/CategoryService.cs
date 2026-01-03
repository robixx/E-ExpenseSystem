using Expense.Application.Interface;
using Expense.Application.ModelViews;
using Expense.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expense.Infrastructure.Service
{
    public class CategoryService : ICategory
    {
        private readonly DatabaseConnection _connection;

        public CategoryService(DatabaseConnection connection)
        {
            _connection = connection;
        }

        public async Task<(string Message, bool Status)> AddCategoryAsync(CategoryDto category)
        {
            try
            {
                string mes = "";
                bool st = false;
                if (category.CategoryId > 0)
                {
                    var data = await _connection.Category.Where(i => i.CategoryId == category.CategoryId).FirstOrDefaultAsync();
                    if (data != null)
                    {
                        data.CategoryName = category.CategoryName;
                        data.CategoryDescription = category.CategoryDescription??"";
                        data.IsActive = category.IsActive == true ? 1 : 0;

                    }
                    mes = $"{category.CategoryName} Updated Successfuly";
                    st = true;

                }
                else
                {
                    var item = new Category
                    {
                        CategoryName=category.CategoryName,
                        CategoryDescription=category.CategoryDescription ?? "",
                        IsActive=category.IsActive==true ?1 :0,
                        CreateDate=DateTime.Now,
                        CreateBy=category.CreateBy,
                    };
                    mes = $"{category.CategoryName} Create Successfuly";
                    st = true;
                    await _connection.Category.AddRangeAsync(item);
                    
                }
                await _connection.SaveChangesAsync();
                return (mes, st);
            }catch (Exception ex)
            {
                return ($"Error:{ex.Message}", false);
            }
        }

        public async Task<(string Message, bool Status, List<CategoryDto> datalist)> CategoryListAsync(string userId)
        {
            try
            {
                int UserId= Convert.ToInt32(userId);

                var list= await _connection.Category.Where(i=>i.CreateBy== UserId)
                    .Select(c => new CategoryDto
                    {
                        CategoryId = c.CategoryId,
                        CategoryName = c.CategoryName,
                        CategoryDescription = c.CategoryDescription??"",
                        CreateDate = c.CreateDate,
                        IsActive = c.IsActive==1?true:false,
                    })
                    .ToListAsync();
                if (list.Count > 0)
                {
                    return ("Data Retrived Successfully", true, list);
                }

                return ("Not Found Data", false, new List<CategoryDto>());

            }catch(Exception ex)
            {
                return ($"Error:{ex.Message}", false, new List<CategoryDto>());
            }
        }
    }
}
