using OrangeBricks.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrangeBricks.Web.Controllers.Offers.ViewModels
{
    public class OfferViewModel
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public OfferStatus Status { get; set; }
        public bool IsPending => this.Status == OfferStatus.Pending;
        public string StatusClass => this.Status == OfferStatus.Accepted ? "alert-success" : "alert-info";
    }
}