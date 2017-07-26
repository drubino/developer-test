using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using OrangeBricks.Web.Controllers.Offers.ViewModels;
using OrangeBricks.Web.Models;

namespace OrangeBricks.Web.Controllers.Offers.Builders
{
    public class OffersOnPropertyViewModelBuilder
    {
        private readonly IOrangeBricksContext _context;

        public OffersOnPropertyViewModelBuilder(IOrangeBricksContext context)
        {
            _context = context;
        }

        public OffersOnPropertyViewModel Build(int id)
        {
            var property = _context.Properties
                .Where(p => p.Id == id)
                .Include(x => x.Offers)
                .SingleOrDefault();

            var offers = property.Offers ?? new List<Offer>();

            return new OffersOnPropertyViewModel
            {
                HasOffers = offers.Any(),
                Offers = offers.Select(x => new OfferViewModel
                {
                    Id = x.Id,
                    Amount = x.Amount,
                    CreatedAt = x.CreatedAt,
                    Status = x.Status
                }),
                PropertyId = property.Id, 
                PropertyType = property.PropertyType,
                StreetName = property.StreetName,
                Description = property.Description,
                NumberOfBedrooms = property.NumberOfBedrooms
            };
        }
    }
}