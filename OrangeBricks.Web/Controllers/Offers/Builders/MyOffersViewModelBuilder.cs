using System.Linq;
using OrangeBricks.Web.Controllers.Offers.ViewModels;
using OrangeBricks.Web.Models;
using System.Data.Entity;

namespace OrangeBricks.Web.Controllers.Offers.Builders
{
    internal class MyOffersViewModelBuilder
    {
        private string _username;
        private IOrangeBricksContext _context;

        public MyOffersViewModelBuilder(string username, IOrangeBricksContext context)
        {
            _username = username;
            _context = context;
        }

        public MyOffersViewModel Build()
        {
            var offers = _context.Offers
                .Include(x => x.Property)
                .Include(x => x.Property.Location)
                .Where(x => x.Status != OfferStatus.Removed)
                .Where(o => o.Username == _username)
                .ToList();

            var viewModel = new MyOffersViewModel
            {
                OffersOnProperty = offers.Select(x => new OffersOnPropertyViewModel
                {
                    HasOffers = true,
                    Offers = new OfferViewModel[]
                    {
                        new OfferViewModel
                        {
                            Id = x.Id,
                            Amount = x.Amount,
                            CreatedAt = x.CreatedAt,
                            Status = x.Status
                        }
                    },
                    PropertyId = x.Property.Id,
                    PropertyType = x.Property.PropertyType,
                    StreetName = x.Property.StreetName,
                    Location = x.Property.Location.Name,
                    Description = x.Property.Description,
                    NumberOfBedrooms = x.Property.NumberOfBedrooms
                })
                .ToList()
            };

            return viewModel;
        }
    }
}