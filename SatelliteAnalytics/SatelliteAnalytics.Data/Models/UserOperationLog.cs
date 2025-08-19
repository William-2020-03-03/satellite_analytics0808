using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatelliteAnalytics.Data.Models
{

    [Table("user_operation_log")]
    public class UserOperationLog
    {
        [Key]
        [Required]
        [Column("id")]
        public string Id { get; set; }

        [Required]
        [Column("user_id")]
        public string UserId { get; set; }

        [Required]
        [Column("application_id")]
        public string ApplicationId { get; set; }

        [Required]
        [Column("module")]
        public string Module { get; set; }

        [Required]
        [Column("operation")]
        public string Operation { get; set; }

        [Required]
        [Column("trigger_type")]
        public string TriggerType { get; set; }

        [Required]
        [Column("browser")]
        public string Browser { get; set; }

        [Required]
        [Column("created")]
        public DateTime Created { get; set; }




        [ForeignKey("ApplicationId")]
        public ApplicationInfo Application { get; set; }
    }
}
