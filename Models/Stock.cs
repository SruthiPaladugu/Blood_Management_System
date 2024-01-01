using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BloodManagementSystem_MVC_.Models
{
    public class Stock
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DonorId { get; set; }

        [Required]
        [StringLength(10)]
        public string BloodGroup { get; set; }

        [Required]
        [StringLength(50)]
        public string DonorName { get; set; }

        public int DonorContact { get; set; }

        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        public DateTime? DonatedDate { get; set; }

        public virtual Donor Donor { get; set; }
    }
}