using System.Linq;
using OrangeBricks.Web.Controllers.Property.ViewModels;
using OrangeBricks.Web.Models;
using System.Data.Entity;

namespace OrangeBricks.Web.Controllers.Property.Builders
{
    public class MyPropertiesViewModelBuilder
    {
        private readonly string _userId;
        private readonly IOrangeBricksContext _context;

        public MyPropertiesViewModelBuilder(string userId, IOrangeBricksContext context)
        {
            _userId = userId;
            _context = context;
        }

        public MyPropertiesViewModel Build()
        {
            return new MyPropertiesViewModel
            {
                Properties = _context.Properties
                    .Include(p => p.Location)
                    .Where(p => p.SellerUserId == _userId)
                    .Select(p => new PropertyViewModel
                    {
                        Id = p.Id,
                        StreetName = p.StreetName,
                        Location = p.Location.Name,
                        Description = p.Description,
                        NumberOfBedrooms = p.NumberOfBedrooms,
                        PropertyType = p.PropertyType,
                        IsListedForSale = p.IsListedForSale,
                        OfferCount = p.Offers
                            .Where(o => o.Status != OfferStatus.Removed && o.Status != OfferStatus.Rejected)
                            .Count(),
                        ViewingCount = p.Viewings
                            .Where(o => o.Status != ViewingStatus.Removed && o.Status != ViewingStatus.Cancelled)
                            .Count()
                    })
                    .ToList()
            };
        }
    }
}