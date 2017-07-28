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
            var removable = offer.Status == OfferStatus.Rejected || offer.Status == OfferStatus.Pending;
            if (!removable)
                throw new InvalidOperationException("Only offers that are pending or have been rejected can be removed.");

            offer.UpdatedAt = DateTime.Now;
            offer.Status = OfferStatus.Removed;

            _context.SaveChanges();
        }
    }
}