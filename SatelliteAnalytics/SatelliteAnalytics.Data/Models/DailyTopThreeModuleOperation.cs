using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatelliteAnalytics.Data.Models
{
    [Table("Top3ModuleOperation_Temp")]
    public class DailyTopThreeModuleOperation
    {
        [Key]
        [Required]
        [Column("Id")]
        public Guid Id { get; set; }

        [Column("Module")]
        public string Module { get; set; }

        [Column("Operation")]
        public string Operation { get; set; }

        [Column("MyCount")]
        public int MyCount { get; set; }
    }
}
