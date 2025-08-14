using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using SatelliteAnalytics.DTO;
using SatelliteAnalytics.Redis;
using SatelliteAnalytics.Services;
using System.Text.Json;

namespace SatelliteAnalytics.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RedisController : Controller
    {
        private readonly IStatsCacheService _cacheService;

        public RedisController(IStatsCacheService service)
        {
            _cacheService = service;
        }

        [HttpGet("application/Redis/Top3ByModuleAndOperation")]
        public async Task<ActionResult<IEnumerable<TopThreeModuleOperation>>> GetTop3ByModuleAndOperation()
        {
            // 按 UTC 日期拼 key
            string key = $"top3moduleoperation:summary:{DateTime.UtcNow:yyyyMMdd}";

            var value = await _cacheService.GetStatsAsync(key);
            var result = JsonSerializer.Deserialize<List<TopThreeModuleOperation>>(value);

            return Ok(result);
        }
    }
}
