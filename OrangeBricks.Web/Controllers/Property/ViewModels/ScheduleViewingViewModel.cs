using System;
using System.Linq;

namespace OrangeBricks.Web.Controllers.Property.ViewModels
{
    public class ScheduleViewingViewModel
    {
        public int PropertyId { get; set; }
        public string PropertyType { get; set; }
        public string PropertyLocation { get; set; }
        public string PropertyTimeZone { get; set; }
        public string PropertyTimeAbbreviation { get; set; }
        public string ViewingDate { get; set; }
        public string ViewingTime { get; set; }
    }
}