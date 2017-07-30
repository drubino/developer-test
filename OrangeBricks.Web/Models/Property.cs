using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrangeBricks.Web.Models
{
    public class Property
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string PropertyType { get; set; }

        [Required]
        public string StreetName { get; set; }

        [Required]
        public string LocationName { get; set; }

        [ForeignKey(nameof(LocationName))]
        public Location Location { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int NumberOfBedrooms { get; set; }

        [Required]
        public string SellerUserId { get; set; }

        public bool IsListedForSale { get; set; }

        public ICollection<Offer> Offers { get; set; }
        public ICollection<Viewing> Viewings { get; set; }
    }
}