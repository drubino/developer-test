﻿using System.Linq;
using OrangeBricks.Web.Controllers.Offers.ViewModels;
using OrangeBricks.Web.Models;
using System.Data.Entity;

namespace OrangeBricks.Web.Controllers.Offers.Builders
{
    internal class MyOffersViewModelBuilder
    {
        private string _userId;
        private IOrangeBricksContext _context;

        public MyOffersViewModelBuilder(string userId, IOrangeBricksContext context)
        {
            _userId = userId;
            _context = context;
        }

        public MyOffersViewModel Build()
        {
            var offers = _context.Offers
                .Include(o => o.Property)
                .Include(o => o.Property.Location)
                .Where(o => o.Status != OfferStatus.Removed)
                .Where(o => o.UserId == _userId)
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