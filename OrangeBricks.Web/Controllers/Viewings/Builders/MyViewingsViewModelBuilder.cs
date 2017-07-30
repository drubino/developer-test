using System.Linq;
using OrangeBricks.Web.Controllers.Viewings.ViewModels;
using OrangeBricks.Web.Models;
using System.Data.Entity;
using System;

namespace OrangeBricks.Web.Controllers.Viewings.Builders
{
    internal class MyViewingsViewModelBuilder
    {
        private string _userId;
        private IOrangeBricksContext _context;

        public MyViewingsViewModelBuilder(string userId, IOrangeBricksContext context)
        {
            _userId = userId;
            _context = context;
        }

        public MyViewingsViewModel Build()
        {
            var viewings = _context.Viewings
                .Include(o => o.Property)
                .Include(o => o.Property.Location)
                .Where(o => o.Status != ViewingStatus.Removed)
                .Where(o => o.UserId == _userId)
                .ToList();

            var viewModel = new MyViewingsViewModel
            {
                ViewingsForProperty = viewings.Select(v => new ViewingsForPropertyViewModel
                {
                    HasViewings = true,
                    Viewings = new ViewingViewModel[]
                    {
                        new ViewingViewModel
                        {
                            Id = v.Id,
                            ViewingDate = FormatDate(v.Date),
                            ViewingTime = FormatTime(v.Date, v.Property.Location.TimeZone),
                            Status = v.Status
                        }
                    },
                    PropertyId = v.Property.Id,
                    PropertyType = v.Property.PropertyType,
                    StreetName = v.Property.StreetName,
                    Location = v.Property.Location.Name,
                    Description = v.Property.Description,
                    NumberOfBedrooms = v.Property.NumberOfBedrooms
                })
                .ToList()
            };

            return viewModel;
        }

        public string FormatDate(DateTime date) => date.ToString("D");
        public string FormatTime(DateTime date, string propertyTimeZone)
        {
            var timeZoneAbbrevation = string.Join("", propertyTimeZone.Split(' ').Select(s => s.Substring(0, 1))).ToUpper();
            var timeString = $"{date.ToString("hh:mm tt")} {timeZoneAbbrevation}";
            if (timeString.StartsWith("0"))
                timeString = timeString.Substring(1);

            return timeString;
        }
    }
}