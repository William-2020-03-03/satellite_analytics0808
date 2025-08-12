using Microsoft.EntityFrameworkCore;
using SatelliteAnalytics.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatelliteAnalytics.Data.Repositories
{
    public class UserOperationLogRepository_test : Repository_test<UserOperationLog>, IUserOperationLogRepository_test
    {
        public UserOperationLogRepository_test(SatelliteAnalyticsDBContext context) : base(context)
        {

        }

        //public async Task<List<TopThreeModuleOperation>> GetTop3ByModuleAndOperation()
        //{
        //    List<TopThreeModuleOperation> ret = this._dbSet
        //        .GroupBy(item => new { item.ModuleName, item.OperationName }, (key, grp) => new TopThreeModuleOperation  () { Operation = key.OperationName , Module = key.ModuleName, Count = grp.Count() })
        //        .OrderBy(item => item.Count)
        //        .ToList();


        //    return ret;
        //}

    }
}
