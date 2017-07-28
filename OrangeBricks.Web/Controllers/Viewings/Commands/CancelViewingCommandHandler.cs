using System;
using OrangeBricks.Web.Models;

namespace OrangeBricks.Web.Controllers.Viewings.Commands
{
    public class CancelViewingCommandHandler
    {
        private readonly IOrangeBricksContext _context;

        public CancelViewingCommandHandler(IOrangeBricksContext context)
        {
            _context = context;
        }

        public void Handle(CancelViewingCommand command)
        {
            var viewing = _context.Viewings.Find(command.ViewingId);

            viewing.UpdatedAt = DateTime.Now;
            viewing.Status = ViewingStatus.Cancelled;

            _context.SaveChanges();
        }
    }
}