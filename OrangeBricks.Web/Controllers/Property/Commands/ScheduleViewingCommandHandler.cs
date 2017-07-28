using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using OrangeBricks.Web.Models;
using System.Security.Principal;
using Microsoft.AspNet.Identity;

namespace OrangeBricks.Web.Controllers.Property.Commands
{
    public class ScheduleViewingCommandHandler
    {
        private readonly string _username;
        private readonly IOrangeBricksContext _context;

        public ScheduleViewingCommandHandler(string username, IOrangeBricksContext context)
        {
            _username = username;
            _context = context;
        }

        public void Handle(ScheduleViewingCommand command)
        {
            var property = _context.Properties
                .Include(x => x.Offers)
                .FirstOrDefault(p => p.Id == command.PropertyId);

            var viewingDate =
                DateTime.Parse(command.ViewingDate).Date +
                DateTime.Parse(command.ViewingTime).TimeOfDay;

            var viewing = new Viewing
            {
                PropertyId = property.Id,
                Username = _username,
                Date = viewingDate,
                Status = ViewingStatus.Scheduled,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            _context.Viewings.Add(viewing);
            _context.SaveChanges();
        }
    }
}