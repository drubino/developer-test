using System.Data.Entity;
using NSubstitute;
using NUnit.Framework;
using OrangeBricks.Web.Models;
using OrangeBricks.Web.Controllers.Viewings.Commands;
using System.Linq;

namespace OrangeBricks.Web.Tests.Controllers.Viewings.Commands
{
    [TestFixture]
    public class CancelViewingCommandHandlerTest
    {
        private CancelViewingCommandHandler _handler;
        private IOrangeBricksContext _context;
        private Viewing _viewing = new Viewing { Id = 0, Status = ViewingStatus.Scheduled };

        [SetUp]
        public void SetUp()
        {
            _context = Substitute.For<IOrangeBricksContext>();
            _context.Viewings.Find(0).Returns(_viewing);
            _handler = new CancelViewingCommandHandler(_context);
        }

        [Test]
        public void HandleShouldAddProperty()
        {
            // Arrange
            var command = new CancelViewingCommand { ViewingId = 0 };

            // Act
            _handler.Handle(command);

            // Assert
            Assert.That(_viewing.Status, Is.EqualTo(ViewingStatus.Cancelled));
        }
    }
}
