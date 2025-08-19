using SatelliteAnalytics.Data.Models;
using SatelliteAnalytics.DTO;

namespace SatelliteAnalytics.Services
{
    public interface IUserOperationLogService
    {
        public Task<List<UserOperationLog>> GetApplicationLogsAsync(string appId, int page, int pageSize);


        public Task<List<TopThreeModuleOperation>> GetTop3ByModuleAndOperation();


        public Task<List<LogAppInfoDTO>> GetBigDataByPaging(int skip, int take);
    }
}
