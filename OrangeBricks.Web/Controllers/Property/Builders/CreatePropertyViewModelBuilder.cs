using OrangeBricks.Web.Controllers.Property.ViewModels;
using OrangeBricks.Web.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace OrangeBricks.Web.Controllers.Property.Builders
{
    public class CreatePropertyViewModelBuilder
    {
        private readonly IOrangeBricksContext _context;

        public CreatePropertyViewModelBuilder(IOrangeBricksContext context)
        {
            _context = context;
        }

        public CreatePropertyViewModel Build()
        {
            var locations = _context.Locations.ToList();

            var viewModel = new CreatePropertyViewModel()
            {
                PossiblePropertyTypes = new string[] { "House", "Flat", "Bungalow" }
                    .Select(x => new SelectListItem { Value = x, Text = x })
                    .AsEnumerable(),
                PossibleLocations = locations
                    .Select(l => new SelectListItem { Value = l.Name, Text = l.Name })
                    .ToList()
            };

            return viewModel;
        }
    }
}