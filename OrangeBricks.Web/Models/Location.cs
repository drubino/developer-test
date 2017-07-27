using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace OrangeBricks.Web.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TimeZone { get; set; }
    }
}