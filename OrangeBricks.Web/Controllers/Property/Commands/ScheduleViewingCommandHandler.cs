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
        private readonly string _userId;
        private readonly IOrangeBricksContext _context;

        public ScheduleViewingCommandHandler(string userId, IOrangeBricksContext context)
        {
            _userId = userId;
            _context = context;
        }

        public void Handle(ScheduleViewingCommand command)
        {
            var property = _context.Properties
                .Include(x => x.Offers)
                .Include(x => x.Viewings)
                .FirstOrDefault(p => p.Id == command.PropertyId);

            var hasOffers = property.Offers
                .Where(o => o.Status != OfferStatus.Removed)
                .Any();

            if (hasOffers)
                throw new InvalidOperationException("The user already has already made an offer on the property");

            var existingViewing = property.Viewings
                .Where(o => o.Status != ViewingStatus.Removed)
                .SingleOrDefault(o => o.UserId == _userId);

            if (existingViewing != null)
                throw new InvalidOperationException("The user already has an active viewing on the property");

            var viewingDate =
                DateTime.Parse(command.ViewingDate).Date +
                DateTime.Parse(command.ViewingTime).TimeOfDay;

            var viewing = new Viewing
            {
                PropertyId = property.Id,
                UserId = _userId,
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