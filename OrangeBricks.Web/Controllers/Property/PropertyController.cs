﻿using System.Collections;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using OrangeBricks.Web.Attributes;
using OrangeBricks.Web.Controllers.Property.Builders;
using OrangeBricks.Web.Controllers.Property.Commands;
using OrangeBricks.Web.Controllers.Property.ViewModels;
using OrangeBricks.Web.Models;
using System;

namespace OrangeBricks.Web.Controllers.Property
{
    public class PropertyController : Controller
    {
        private readonly IOrangeBricksContext _context;

        public PropertyController(IOrangeBricksContext context)
        {
            _context = context;
        }

        public ActionResult Index(PropertiesQuery query)
        {
            var username = this.User.Identity.GetUserId();
            var builder = new PropertiesViewModelBuilder(username, _context);
            var viewModel = builder.Build(query);

            return View(viewModel);
        }

        [Authorize]
        [OrangeBricksAuthorize(Roles = "Seller")]
        public ActionResult Create()
        {
            var builder = new CreatePropertyViewModelBuilder(_context);
            var viewModel = builder.Build();

            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        [OrangeBricksAuthorize(Roles = "Seller")]
        public ActionResult Create(CreatePropertyCommand command)
        {
            var handler = new CreatePropertyCommandHandler(_context);
            command.SellerUserId = User.Identity.GetUserId();
            handler.Handle(command);

            return RedirectToAction("MyProperties");
        }

        [Authorize]
        [OrangeBricksAuthorize(Roles = "Seller")]
        public ActionResult MyProperties()
        {
            var userId = User.Identity.GetUserId();
            var builder = new MyPropertiesViewModelBuilder(userId, _context);
            var viewModel = builder.Build();

            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        [OrangeBricksAuthorize(Roles = "Seller")]
        public ActionResult ListForSale(ListPropertyCommand command)
        {
            var handler = new ListPropertyCommandHandler(_context);

            handler.Handle(command);

            return RedirectToAction("MyProperties");
        }

        [Authorize]
        [OrangeBricksAuthorize(Roles = "Buyer")]
        public ActionResult MakeOffer(int id)
        {
            var builder = new MakeOfferViewModelBuilder(_context);
            var viewModel = builder.Build(id);
            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        [OrangeBricksAuthorize(Roles = "Buyer")]
        public ActionResult MakeOffer(MakeOfferCommand command)
        {
            var username = this.User.Identity.GetUserId();
            var handler = new MakeOfferCommandHandler(username, _context);
            handler.Handle(command);

            return RedirectToAction("Index");
        }

        [Authorize]
        [OrangeBricksAuthorize(Roles = "Buyer")]
        public ActionResult ScheduleViewing(int id)
        {
            var builder = new ScheduleViewingViewModelBuilder(_context);
            var viewModel = builder.Build(id);
            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        [OrangeBricksAuthorize(Roles = "Buyer")]
        public ActionResult ScheduleViewing(ScheduleViewingCommand command)
        {
            var username = this.User.Identity.GetUserId();
            var handler = new ScheduleViewingCommandHandler(username, _context);
            handler.Handle(command);
            
            return RedirectToAction("Index");
        }
    }
}