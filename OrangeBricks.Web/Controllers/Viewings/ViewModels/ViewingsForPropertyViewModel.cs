using System;
using System.Collections.Generic;

namespace OrangeBricks.Web.Controllers.Viewings.ViewModels
{
    public class ViewingsForPropertyViewModel
    {
        public int PropertyId { get; set; }
        public string PropertyType { get; set; }
        public int NumberOfBedrooms{ get; set; }
        public string StreetName { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public bool HasViewings { get; set; }
        public IEnumerable<ViewingViewModel> Viewings { get; set; }
    }
}