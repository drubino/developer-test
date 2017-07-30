using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrangeBricks.Web.Models
{
    public class Location
    {
        [Key]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string TimeZone { get; set; }
    }
}