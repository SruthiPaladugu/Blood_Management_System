using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BloodManagementSystem_MVC_.Models
{
    public class Seeker
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Seeker()
        {
            Requests = new HashSet<Request>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SeekerId { get; set; }

        [Required]
        [StringLength(50)]
        public string SeekerName { get; set; }

        public int SeekerPhone { get; set; }

        [Required]
        [StringLength(10)]
        public string SeekerBloodGroup { get; set; }

        [StringLength(10)]
        public string SeekerLocation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Request> Requests { get; set; }
    }
}