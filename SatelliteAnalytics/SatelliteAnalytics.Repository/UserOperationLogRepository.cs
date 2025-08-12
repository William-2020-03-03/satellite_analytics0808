using Microsoft.EntityFrameworkCore;
using SatelliteAnalytics.Data;
using SatelliteAnalytics.Data.Models;
using SatelliteAnalytics.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatelliteAnalytics.Repository
{
    public class UserOperationLogRepository : IUserOperationLogRepository
    {

        private readonly SatelliteAnalyticsDBContext _context;


        public UserOperationLogRepository(SatelliteAnalyticsDBContext context)
        {
            this._context = context;
        }



        public async Task<List<TopThreeModuleOperation>> GetTop3ByModuleAndOperation_1()
        {
            List<TopThreeModuleOperation> ret = this._context.UserOperationLogs
                .GroupBy(item => new { item.Module, item.Operation }, (key, grp) => new TopThreeModuleOperation() { Module = key.Module, Operation = key.Operation, Count = grp.Count() })
                .OrderByDescending(item => item.Count)
                .Take(3)
                .ToList();


            return ret;
        }


        public async Task<List<TopThreeModuleOperation>> GetTop3ByModuleAndOperation()
        {
            List<TopThreeModuleOperation> ret = this._context.DailyTopThreeModuleOperations
                .Select(item => new TopThreeModuleOperation() { Module = item.Module, Operation = item.Operation, Count = item.MyCount })
                .ToList();

            return ret;
        }




        public Task<List<UserOperationLog>> GetLogsByApplicationIdAsync(string appId, int page, int pageSize)
        {
            //return await _context.UserOperationLogs
            //    .Include(u => u.Application)
            //    .Where(u => u.ApplicationId == appId)
            //    .OrderByDescending(u => u.Id)
            //    .Skip((page - 1) * pageSize)
            //    .Take(pageSize)
            //    .AsNoTracking() // 大数据量优化
            //    .ToListAsync();

            return null;
        }

    }
}
