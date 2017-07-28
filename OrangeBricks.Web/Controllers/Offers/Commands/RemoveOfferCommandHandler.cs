using System;
using OrangeBricks.Web.Models;

namespace OrangeBricks.Web.Controllers.Offers.Commands
{
    public class RemoveOfferCommandHandler
    {
        private readonly IOrangeBricksContext _context;

        public RemoveOfferCommandHandler(IOrangeBricksContext context)
        {
            _context = context;
        }

        public void Handle(RemoveOfferCommand command)
        {
            var offer = _context.Offers.Find(command.OfferId);
            if (offer.Status != OfferStatus.Rejected)
                throw new InvalidOperationException("Only offers that have been rejected can be removed.");

            offer.UpdatedAt = DateTime.Now;
            offer.Status = OfferStatus.Removed;

            _context.SaveChanges();
        }
    }
}