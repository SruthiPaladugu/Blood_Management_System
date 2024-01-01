using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BloodManagementSystem_MVC_.Models
{
    public class Admin
    {
        [StringLength(10)]
        public string AdminId { get; set; }

        [StringLength(10)]
        public string Adminpassword { get; set; }
    }
}