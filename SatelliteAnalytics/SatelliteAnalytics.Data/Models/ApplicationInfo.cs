using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatelliteAnalytics.Data.Models
{
    [Table("application_info")]
    public class ApplicationInfo
    {
        [Key]
        [Required]
        [Column("id")]
        public string Id { get; set; }

        [Required]
        [Column("build")]
        public string Build { get; set; }

        [Required]
        [Column("platform")]
        public string Platform { get; set; }

        [Required]
        [Column("language")]
        public string Language { get; set; }

        public ICollection<UserOperationLog> UserOperationLogs { get; set; }
    }
}
