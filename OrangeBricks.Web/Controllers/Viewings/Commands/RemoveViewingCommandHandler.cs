using System;
using OrangeBricks.Web.Models;

namespace OrangeBricks.Web.Controllers.Viewings.Commands
{
    public class RemoveViewingCommandHandler
    {
        private readonly IOrangeBricksContext _context;

        public RemoveViewingCommandHandler(IOrangeBricksContext context)
        {
            _context = context;
        }

        public void Handle(RemoveViewingCommand command)
        {
            var viewing = _context.Viewings.Find(command.ViewingId);
            var removable = viewing.Status == ViewingStatus.Scheduled || viewing.Status == ViewingStatus.Cancelled;
            if (!removable)
                throw new InvalidOperationException("Only viewings that are scheduled or have been cancelled can be removed.");

            viewing.UpdatedAt = DateTime.Now;
            viewing.Status = ViewingStatus.Removed;

            _context.SaveChanges();
        }
    }
}