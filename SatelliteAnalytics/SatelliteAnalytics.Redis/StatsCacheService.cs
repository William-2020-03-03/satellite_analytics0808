using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatelliteAnalytics.Redis
{
    public class StatsCacheService : IStatsCacheService
    {
        private readonly IDatabase _redisDb;

        public StatsCacheService(IConnectionMultiplexer redis)
        {
            _redisDb = redis.GetDatabase();
        }

        public async Task SetStatsAsync(string key, string value, TimeSpan expiry)
        {
            await _redisDb.StringSetAsync(key, value, expiry);
        }

        public async Task<string> GetStatsAsync(string key)
        {
            return await _redisDb.StringGetAsync(key);
        }
    }
}
