using Microsoft.EntityFrameworkCore;
using SatelliteAnalytics.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatelliteAnalytics.Data
{
    public class SatelliteAnalyticsDBContext : DbContext
    {

        public SatelliteAnalyticsDBContext(DbContextOptions<SatelliteAnalyticsDBContext> options) : base(options) { }

        public DbSet<UserOperationLog> UserOperationLogs { get; set; }
        public DbSet<ApplicationInfo> ApplicationInfos { get; set; }

        public DbSet<DailyTopThreeModuleOperation> DailyTopThreeModuleOperations { get; set; }




        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=localhost;Database=lr_satellite;Trusted_Connection=True;");
        //}
    }
}
