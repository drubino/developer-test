using OrangeBricks.Web.Models;

namespace OrangeBricks.Web.Controllers.Property.ViewModels
{
    public class PropertyViewModel
    {
        public string StreetName { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public int NumberOfBedrooms { get; set; }
        public string PropertyType { get; set; }
        public int Id { get; set; }
        public bool IsListedForSale { get; set; }
        public int OfferCount { get; set; }
        public int ViewingCount { get; set; }
        public bool HasOffers => this.OfferCount > 0;
        public bool HasViewings => this.ViewingCount > 0;
    }
}