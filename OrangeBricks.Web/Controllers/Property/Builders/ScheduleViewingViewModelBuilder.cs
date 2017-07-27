using OrangeBricks.Web.Controllers.Property.ViewModels;
using OrangeBricks.Web.Models;
using System;

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
            var property = _context.Properties.Find(id);

            return new ScheduleViewingViewModel
            {
                PropertyId = property.Id,
                ViewingDate = DateTime.UtcNow
            };
        }
    }
}