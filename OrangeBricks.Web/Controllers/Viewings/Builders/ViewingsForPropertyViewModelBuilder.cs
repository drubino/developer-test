using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using OrangeBricks.Web.Controllers.Viewings.ViewModels;
using OrangeBricks.Web.Models;
using System;

namespace OrangeBricks.Web.Controllers.Viewings.Builders
{
    public class ViewingsForPropertyViewModelBuilder
    {
        private readonly IOrangeBricksContext _context;

        public ViewingsForPropertyViewModelBuilder(IOrangeBricksContext context)
        {
            _context = context;
        }

        public ViewingsForPropertyViewModel Build(int id)
        {
            var property = _context.Properties
                .Include(x => x.Viewings)
                .Include(x => x.Location)
                .Where(p => p.Id == id)
                .SingleOrDefault();

            var offers = property.Viewings ?? new List<Viewing>();

            return new ViewingsForPropertyViewModel
            {
                HasViewings = offers.Any(),
                Viewings = offers
                    .Where(o => o.Status == ViewingStatus.Scheduled || o.Status == ViewingStatus.Booked)
                    .Select(v => new ViewingViewModel
                    {
                        Id = v.Id,
                        ViewingDate = FormatDate(v.Date),
                        ViewingTime = FormatTime(v.Date, v.Property.Location.TimeZone),
                        Status = v.Status
                    }),
                PropertyId = property.Id, 
                PropertyType = property.PropertyType,
                StreetName = property.StreetName,
                Location = property.Location.Name,
                Description = property.Description,
                NumberOfBedrooms = property.NumberOfBedrooms
            };
        }

        public string FormatDate(DateTime date) => date.ToString("D");
        public string FormatTime(DateTime date, string propertyTimeZone)
        {
            var timeZoneAbbrevation = string.Join("", propertyTimeZone.Split(' ').Select(s => s.Substring(0, 1))).ToUpper();
            var timeString = $"{date.ToString("hh:mm:ss tt")} {timeZoneAbbrevation}";
            if (timeString.StartsWith("0"))
                timeString = timeString.Substring(1);

            return timeString;
        }
    }
}