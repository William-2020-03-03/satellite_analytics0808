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


        public async Task<List<LogAppInfoDTO>> GetBigDataByPaging(int skip = 0, int take=1000)
        {
            //var ret1 = this._context.UserOperationLogs.Join(this._context.ApplicationInfos, u => u.ApplicationId, p => p.Id, (u, p) => new LogAppInfoDTO()
            //{
            //    Module = u.Module,
            //    Operation = u.Operation,
            //    TriggerType = u.TriggerType,
            //    Browser = u.Browser,
            //    Build = u.Application.Build,
            //    Platform = u.Application.Platform,
            //    Language = u.Application.Language,
            //})
            //    .Skip(skip)
            //    .Take(take)
            //    .ToList();


            //return ret1;


            List<LogAppInfoDTO> ret = _context.UserOperationLogs
                .OrderByDescending (item => item.Created)
            .Select(u => new LogAppInfoDTO()
            {
                Module = u.Module,
                Operation = u.Operation,
                TriggerType = u.TriggerType,
                Browser = u.Browser,
                Build = u.Application.Build,
                Platform = u.Application.Platform,
                Language = u.Application.Language,
                Created = u.Created,
            })
            .Skip(skip)
            .Take(take)
            .ToList();

            return ret;
        }



    }
}
