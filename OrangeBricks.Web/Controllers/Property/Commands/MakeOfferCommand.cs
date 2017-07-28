using System.ComponentModel.DataAnnotations;

namespace OrangeBricks.Web.Controllers.Property.Commands
{
    public class MakeOfferCommand
    {
        [Required]
        public int PropertyId { get; set; }

        [Required]
        public int Offer { get; set; }
    }
}