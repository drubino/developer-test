using System;
using System.Collections.Generic;

namespace OrangeBricks.Web.Controllers.Offers.ViewModels
{
    public class OffersOnPropertyViewModel
    {
        public string PropertyType { get; set; }
        public int NumberOfBedrooms{ get; set; }
        public string StreetName { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public bool HasOffers { get; set; }
        public IEnumerable<OfferViewModel> Offers { get; set; }
        public int PropertyId { get; set; }
    }
}