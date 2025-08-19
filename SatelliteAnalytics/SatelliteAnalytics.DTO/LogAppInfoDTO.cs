using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatelliteAnalytics.DTO
{
    public class LogAppInfoDTO
    {
        public string Module { get; set; }

        public string Operation { get; set; }

        public string TriggerType { get; set; }

        public string Browser { get; set; }

        public string Build { get; set; }

        public string Platform { get; set; }

        public string Language { get; set; }

        public DateTime Created { get; set; }
    }
}
