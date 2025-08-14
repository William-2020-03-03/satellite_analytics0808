using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatelliteAnalytics.Redis
{
    public interface IStatsCacheService
    {
        Task SetStatsAsync(string key, string value, TimeSpan expiry);
        Task<string> GetStatsAsync(string key);
    }
}
