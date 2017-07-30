using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using OrangeBricks.Web.Models;
using System.Security.Principal;
using Microsoft.AspNet.Identity;

namespace OrangeBricks.Web.Controllers.Property.Commands
{
    public class MakeOfferCommandHandler
    {
        private readonly string _userId;
        private readonly IOrangeBricksContext _context;

        public MakeOfferCommandHandler(string userId, IOrangeBricksContext context)
        {
            _userId = userId;
            _context = context;
        }

        public void Handle(MakeOfferCommand command)
        {
            var property = _context.Properties
                .Include(x => x.Offers)
                .FirstOrDefault(p => p.Id == command.PropertyId);

            var existingOffer = property.Offers
                .Where(o => o.Status != OfferStatus.Removed)
                .SingleOrDefault(o => o.UserId == _userId);

            if (existingOffer != null)
                throw new InvalidOperationException("The user already has an active offer on the property");

            var offer = new Offer
            {
                PropertyId = property.Id,
                UserId = _userId,
                Amount = command.Offer,
                Status = OfferStatus.Pending,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            _context.Offers.Add(offer);
            _context.SaveChanges();
        }
    }
}