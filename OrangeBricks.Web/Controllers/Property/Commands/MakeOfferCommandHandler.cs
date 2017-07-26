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
        private readonly IPrincipal _user;
        private readonly IOrangeBricksContext _context;

        public MakeOfferCommandHandler(IPrincipal user, IOrangeBricksContext context)
        {
            _user = user;
            _context = context;
        }

        public void Handle(MakeOfferCommand command)
        {
            var username = _user.Identity.GetUserName();
            var property = _context.Properties
                .Include("Offers")
                .FirstOrDefault(p => p.Id == command.PropertyId);

            var existingOffer = property.Offers.SingleOrDefault(o => o.Username == username);
            if (existingOffer != null)
                throw new InvalidOperationException(
                    string.Format("{0} has already made an offer on property {1}", username, property.Id));

            var offer = new Offer
            {
                PropertyId = property.Id,
                Username = username,
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