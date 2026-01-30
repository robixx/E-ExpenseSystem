using Expense.Application.Interface;
using Expense.Application.ModelViews;
using Expense.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expense.Infrastructure.Service
{
    public class WorkerService : IWorkData
    {
        private readonly DatabaseConnection _connection;
        public WorkerService(DatabaseConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<WorkPlanDto>> getWorkPlan(int CreatedBy)
        {
            try
            {

                var datalist = await _connection.WorkPlan
                             .Where(i => i.CreatedBy == CreatedBy)
                             .OrderByDescending(i=>i.CreateDate)
                             .Select(i => new WorkPlanDto
                             {
                                 Id = i.Id,
                                 ValueType = i.ValueType,
                                 CreatedBy = i.CreatedBy,
                                 IsTrue = i.IsTrue,
                                 CreateDate = i.CreateDate
                             })
                             .ToListAsync();
                return datalist;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<(string Message, bool Status)> SaveWorkPlan(WorkPlanDto work)
        {
            try
            {
                if (work != null)
                {
                    if (work.Id > 0)
                    {
                        var da = await _connection.WorkPlan.FirstOrDefaultAsync(i => i.Id == work.Id);
                        if (da != null)
                        {
                            da.IsTrue = work.IsTrue;
                            da.CreateDate = work.CreateDate;
                        }
                       
                    }
                    else
                    {
                        var wrk = new WorkPlan
                        {
                            IsTrue = 0,
                            ValueType = work?.ValueType ?? "",
                            CreateDate = DateTime.Now,
                            CreatedBy = work?.CreatedBy ?? 1

                        };
                        await _connection.WorkPlan.AddAsync(wrk);
                    }
                       
                    await _connection.SaveChangesAsync();
                    return ("Work Save Successfully", true);
                }


                return ("InValid Data", false);


            }
            catch (Exception ex)
            {
                return (ex.Message, false);
            }
        }
    }
}
