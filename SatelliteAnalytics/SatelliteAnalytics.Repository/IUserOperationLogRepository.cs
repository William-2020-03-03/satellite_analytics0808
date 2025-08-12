using SatelliteAnalytics.Data.Models;
using SatelliteAnalytics.DTO;

namespace SatelliteAnalytics.Repository
{
    public interface IUserOperationLogRepository
    {
        public Task<List<UserOperationLog>> GetLogsByApplicationIdAsync(string appId, int page, int pageSize);

        public Task<List<TopThreeModuleOperation>> GetTop3ByModuleAndOperation();
    }
}
