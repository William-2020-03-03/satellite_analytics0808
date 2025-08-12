using SatelliteAnalytics.Data.Models;
using SatelliteAnalytics.DTO;

namespace SatelliteAnalytics.Services
{
    public interface IUserOperationLogService
    {
        Task<List<UserOperationLog>> GetApplicationLogsAsync(string appId, int page, int pageSize);


        Task<List<TopThreeModuleOperation>> GetTop3ByModuleAndOperation();
    }
}
