using Microsoft.AspNetCore.Mvc;
using SatelliteAnalytics.DTO;
using SatelliteAnalytics.Services;

namespace SatelliteAnalytics.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SatelliteAnalyticsController : Controller
    {
        private readonly IUserOperationLogService _service;

        public SatelliteAnalyticsController(IUserOperationLogService service)
        {
            _service = service;
        }

        [HttpGet("application/Top3ByModuleAndOperation")]
        public async Task<ActionResult<IEnumerable<TopThreeModuleOperation>>> GetTop3ByModuleAndOperation()
        {
            var logs = await _service.GetTop3ByModuleAndOperation();
            return Ok(logs);
        }

        [HttpGet("application/GetBigDataByPaging")]
        public async Task<ActionResult> GetBigDataByPaging(int skip, int take)
        {
            var rets = await _service.GetBigDataByPaging(skip,take);
            return Ok(rets);
        }
    }
}
