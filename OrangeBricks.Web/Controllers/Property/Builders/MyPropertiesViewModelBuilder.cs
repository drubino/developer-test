using System.Linq;
using OrangeBricks.Web.Controllers.Property.ViewModels;
using OrangeBricks.Web.Models;
using System.Data.Entity;

namespace OrangeBricks.Web.Controllers.Property.Builders
{
    public class MyPropertiesViewModelBuilder
    {
        private readonly string _username;
        private readonly IOrangeBricksContext _context;

        public MyPropertiesViewModelBuilder(string username, IOrangeBricksContext context)
        {
            _username = username;
            _context = context;
        }

        public MyPropertiesViewModel Build()
        {
            return new MyPropertiesViewModel
            {
                Properties = _context.Properties
                    .Include(p => p.Location)
                    .Where(p => p.SellerUserId == _username)
                    .Select(p => new PropertyViewModel
                    {
                        Id = p.Id,
                        StreetName = p.StreetName,
                        Location = p.Location.Name,
                        Description = p.Description,
                        NumberOfBedrooms = p.NumberOfBedrooms,
                        PropertyType = p.PropertyType,
                        IsListedForSale = p.IsListedForSale
                    })
                    .ToList()
            };
        }
    }
}