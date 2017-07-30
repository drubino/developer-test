using System.Collections;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using OrangeBricks.Web.Models;
using OrangeBricks.Web.Attributes;
using OrangeBricks.Web.Controllers.Viewings.Commands;
using OrangeBricks.Web.Controllers.Viewings.Builders;

namespace OrangeBricks.Web.Controllers.Viewings
{
    [Authorize]
    public class ViewingsController : Controller
    {
        private readonly IOrangeBricksContext _context;

        public ViewingsController(IOrangeBricksContext context)
        {
            _context = context;
        }

        [OrangeBricksAuthorize(Roles = "Buyer")]
        public ActionResult MyViewings()
        {
            var username = this.User.Identity.GetUserId();
            var builder = new MyViewingsViewModelBuilder(username, _context);
            var viewModel = builder.Build();

            return View(viewModel);
        }

        [OrangeBricksAuthorize(Roles = "Seller")]
        public ActionResult ForProperty(int id)
        {
            var builder = new ViewingsForPropertyViewModelBuilder(_context);
            var viewModel = builder.Build(id);

            return View(viewModel);
        }

        [HttpPost]
        [OrangeBricksAuthorize(Roles = "Seller")]
        public ActionResult Book(BookViewingCommand command)
        {
            var handler = new BookViewingCommandHandler(_context);
            handler.Handle(command);

            return RedirectToAction("ForProperty", new { id = command.PropertyId });
        }

        [HttpPost]
        [OrangeBricksAuthorize(Roles = "Seller")]
        public ActionResult Cancel(CancelViewingCommand command)
        {
            var handler = new CancelViewingCommandHandler(_context);
            handler.Handle(command);

            return RedirectToAction("ForProperty", new { id = command.PropertyId });
        }

        [HttpPost]
        [OrangeBricksAuthorize(Roles = "Buyer")]
        public ActionResult Remove(RemoveViewingCommand command)
        {
            var handler = new RemoveViewingCommandHandler(_context);
            handler.Handle(command);

            return RedirectToAction("MyViewings");
        }
    }
}