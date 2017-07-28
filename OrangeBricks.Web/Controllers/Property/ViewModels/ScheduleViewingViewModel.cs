using System;
using System.Linq;

namespace OrangeBricks.Web.Controllers.Property.ViewModels
{
    public class ScheduleViewingViewModel
    {
        public int PropertyId { get; set; }
        public string PropertyTimeZone { get; set; }
        public DateTime ViewingDate { get; set; }

        public string FormatDate(DateTime date) => date.ToString("D");
        public string FormatTime(DateTime date)
        {
            var timeZoneAbbrevation = string.Join("", this.PropertyTimeZone.Split(' ').Select(s => s.Substring(0, 1))).ToUpper();
            var timeString = $"{date.ToString("hh:mm:ss tt")} {timeZoneAbbrevation}";
            if (timeString.StartsWith("0"))
                timeString = timeString.Substring(1);

            return timeString;
        }
    }
}