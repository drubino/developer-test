using OrangeBricks.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrangeBricks.Web.Controllers.Viewings.ViewModels
{
    public class ViewingViewModel
    {
        public int Id { get; set; }
        public string ViewingDate { get; set; }
        public string ViewingTime { get; set; }
        public ViewingStatus Status { get; set; }
        public bool IsSchedule => this.Status == ViewingStatus.Scheduled;
        public string StatusClass => this.Status == ViewingStatus.Booked ? "alert-success" : "alert-info";
    }
}