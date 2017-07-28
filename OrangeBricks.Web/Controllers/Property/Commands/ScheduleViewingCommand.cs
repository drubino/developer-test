using System.ComponentModel.DataAnnotations;

namespace OrangeBricks.Web.Controllers.Property.Commands
{
    public class ScheduleViewingCommand
    {
        [Required]
        public int PropertyId { get; set; }

        [Required]
        public string ViewingDate { get; set; }

        [Required]
        public string ViewingTime { get; set; }
    }
}