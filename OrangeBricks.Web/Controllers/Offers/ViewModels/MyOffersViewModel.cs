using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrangeBricks.Web.Controllers.Offers.ViewModels
{
    public class MyOffersViewModel
    {
        public IEnumerable<OffersOnPropertyViewModel> OffersOnProperty { get; set; }
    }
}