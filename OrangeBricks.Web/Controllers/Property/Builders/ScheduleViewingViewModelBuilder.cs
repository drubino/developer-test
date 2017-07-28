using OrangeBricks.Web.Controllers.Property.ViewModels;
using OrangeBricks.Web.Models;
using System;
using System.Linq;
using System.Data.Entity;

namespace OrangeBricks.Web.Controllers.Property.Builders
{
    public class ScheduleViewingViewModelBuilder
    {
        private readonly IOrangeBricksContext _context;

        public ScheduleViewingViewModelBuilder(IOrangeBricksContext context)
        {
            _context = context;
        }

        public ScheduleViewingViewModel Build(int id)
        {
            var property = _context.Properties
                .Include(p => p.Location)
                .FirstOrDefault(p => p.Id == id);

            var timeZone = property.Location.TimeZone;
            var timeZoneAbbreviation = string.Join("", timeZone.Split(' ').Select(s => s.Substring(0, 1))).ToUpper();

            return new ScheduleViewingViewModel
            {
                PropertyId = property.Id,
                PropertyType = property.PropertyType,
                PropertyLocation = property.Location.Name,
                PropertyTimeZone = timeZone,
                PropertyTimeAbbreviation = timeZoneAbbreviation
            };
        }
    }
}