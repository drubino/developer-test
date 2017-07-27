﻿using System.Collections;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using OrangeBricks.Web.Models;
using OrangeBricks.Web.Attributes;
using OrangeBricks.Web.Controllers.Viewings.Commands;

namespace OrangeBricks.Web.Controllers.Viewings
{
    public class ViewingsController : Controller
    {
        private readonly IOrangeBricksContext _context;

        public ViewingsController(IOrangeBricksContext context)
        {
            _context = context;
        }

        public ActionResult MyViewings()
        {
            return View();
        }

        [OrangeBricksAuthorize(Roles = "Seller")]
        public ActionResult ForProperty(int id)
        {
            return View();
        }

        [HttpPost]
        [OrangeBricksAuthorize(Roles = "Seller")]
        public ActionResult Book(BookViewingCommand command)
        {
            return RedirectToAction("ForProperty", new { id = command.PropertyId });
        }

        [HttpPost]
        [OrangeBricksAuthorize(Roles = "Seller")]
        public ActionResult Cancel(CancelViewingCommand command)
        {
            return RedirectToAction("ForProperty", new { id = command.PropertyId });
        }

        [HttpPost]
        [OrangeBricksAuthorize(Roles = "Buyer")]
        public ActionResult AcknowledgeCancellation(AcknowledgeCancellationCommand command)
        {
            return RedirectToAction("MyViewings");
        }
    }
}