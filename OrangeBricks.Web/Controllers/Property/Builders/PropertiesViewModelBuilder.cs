using System;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using OrangeBricks.Web.Controllers.Property.ViewModels;
using OrangeBricks.Web.Models;
using System.Security.Principal;
using System.Data.Entity;

namespace OrangeBricks.Web.Controllers.Property.Builders
{
    public class PropertiesViewModelBuilder
    {
        private readonly string _userId;
        private readonly IOrangeBricksContext _context;

        public PropertiesViewModelBuilder(string username, IOrangeBricksContext context)
        {
            _userId = username;
            _context = context;
        }

        public PropertiesViewModel Build(PropertiesQuery query)
        {
            var properties = _context.Properties
                .Where(p => p.IsListedForSale)
                .Select(p => new
                {
                    Property = p,
                    Location = p.Location,
                    OfferCount = p.Offers
                        .Where(o => o.Status != OfferStatus.Removed)
                        .Count(),
                    ViewingCount = p.Viewings
                        .Where(o => o.Status != ViewingStatus.Removed)
                        .Count()
                })
                .ToList();

            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                properties = properties.Where(x => 
                    x.Property.StreetName.Contains(query.Search) || 
                    x.Property.Description.Contains(query.Search))
                    .ToList();
            }

            return new PropertiesViewModel
            {
                Properties = properties.Select(p => MapViewModel(p.Property, p.Location, p.OfferCount, p.ViewingCount)).ToList(),
                Search = query.Search
            };
        }

        private static PropertyViewModel MapViewModel(Models.Property property, Location location, int offerCount, int viewingCount)
        {
            return new PropertyViewModel
            {
                Id = property.Id,
                StreetName = property.StreetName,
                Location = location.Name,
                Description = property.Description,
                NumberOfBedrooms = property.NumberOfBedrooms,
                PropertyType = property.PropertyType,
                OfferCount = offerCount,
                ViewingCount = viewingCount
            };
        }
    }
}