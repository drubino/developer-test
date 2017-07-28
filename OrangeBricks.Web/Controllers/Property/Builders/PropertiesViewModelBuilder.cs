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
        private readonly string _username;
        private readonly IOrangeBricksContext _context;

        public PropertiesViewModelBuilder(string username, IOrangeBricksContext context)
        {
            _username = username;
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
                    Offer = p.Offers
                        .Where(o => o.Status != OfferStatus.Removed)
                        .OrderByDescending(o => o.UpdatedAt)
                        .FirstOrDefault(o => o.Username == _username)
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
                Properties = properties.Select(p => MapViewModel(p.Property, p.Offer, p.Location)).ToList(),
                Search = query.Search
            };
        }

        private static PropertyViewModel MapViewModel(Models.Property property, Offer offer, Location location)
        {
            return new PropertyViewModel
            {
                Id = property.Id,
                StreetName = property.StreetName,
                Location = location.Name,
                Description = property.Description,
                NumberOfBedrooms = property.NumberOfBedrooms,
                PropertyType = property.PropertyType,
                Offer = offer
            };
        }
    }
}