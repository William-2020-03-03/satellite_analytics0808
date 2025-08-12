using Azure;
using SatelliteAnalytics.Data.Models;
using SatelliteAnalytics.DTO;
using SatelliteAnalytics.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatelliteAnalytics.Services
{
    public class UserOperationLogService : IUserOperationLogService
    {
        private readonly IUserOperationLogRepository _repository;

        public UserOperationLogService(IUserOperationLogRepository repository)
        {
            this._repository = repository;
        }

        public Task<List<UserOperationLog>> GetApplicationLogsAsync(string appId, int page, int pageSize)
        {
            return null;
        }

        public async Task<List<TopThreeModuleOperation>> GetTop3ByModuleAndOperation()
        {
            return await _repository.GetTop3ByModuleAndOperation();
        }
    }
}
