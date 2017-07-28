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

            return new ScheduleViewingViewModel
            {
                PropertyId = property.Id,
                PropertyTimeZone = property.Location.TimeZone,
                ViewingDate = DateTime.Now,
            };
        }
    }
}