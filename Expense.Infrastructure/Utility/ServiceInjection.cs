using Expense.Application.Interface;
using Expense.Domain.Entity;
using Expense.Infrastructure.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace Expense.Infrastructure
{
    public static class ServiceInjection
    {
        public static void InjectServices(this IServiceCollection services)
        {

            services.AddScoped<ICategory, CategoryService>();
            services.AddScoped<IUser, UserService>();
        }
    }
}
