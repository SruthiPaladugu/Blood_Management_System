using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BloodManagementSystem_MVC_.Models
{
    public class Request
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int RequestId { get; set; }
        
        public int? SeekerId { get; set; }

        public int? DonorId { get; set; }

        [StringLength(10)]
        public string BloodGroup { get; set; }

        public virtual Donor Donor { get; set; }

        public virtual Seeker Seeker { get; set; }
    }
}