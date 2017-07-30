using System;
using System.Linq;
using OrangeBricks.Web.Models;

namespace OrangeBricks.Web.Controllers.Viewings.Commands
{
    public class BookViewingCommandHandler
    {
        private readonly IOrangeBricksContext _context;

        public BookViewingCommandHandler(IOrangeBricksContext context)
        {
            _context = context;
        }

        public void Handle(BookViewingCommand command)
        {
            var viewing = _context.Viewings.Find(command.ViewingId);

            viewing.UpdatedAt = DateTime.Now;
            viewing.Status = ViewingStatus.Booked;

            _context.SaveChanges();
        }
    }
}