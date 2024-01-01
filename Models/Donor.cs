using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BloodManagementSystem_MVC_.Models
{
    public class Donor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Donor()
        {
            Requests = new HashSet<Request>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DonorId { get; set; }

        [Required]
        [StringLength(50)]
        public string DonorName { get; set; }

        [Required]
        public int DonorPhone { get; set; }

        [Required]
        [StringLength(10)]
        public string DonorBloodGroup { get; set; }

        [StringLength(10)]
        public string DonorLocation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Request> Requests { get; set; }

        public virtual Stock Stock { get; set; }
    }
}