using System.Data.Entity;
using NSubstitute;
using NUnit.Framework;
using OrangeBricks.Web.Models;
using System.Linq;
using OrangeBricks.Web.Controllers.Offers.Commands;

namespace OrangeBricks.Web.Tests.Controllers.Offers.Commands
{
    [TestFixture]
    public class RemoveOfferCommandHandlerTest
    {
        private RemoveOfferCommandHandler _handler;
        private IOrangeBricksContext _context;
        private Offer _offer = new Offer { Id = 0, Status = OfferStatus.Pending };

        [SetUp]
        public void SetUp()
        {
            _context = Substitute.For<IOrangeBricksContext>();
            _context.Offers.Find(0).Returns(_offer);
            _handler = new RemoveOfferCommandHandler(_context);
        }

        [Test]
        public void HandleShouldAddProperty()
        {
            // Arrange
            var command = new RemoveOfferCommand { OfferId = 0 };

            // Act
            _handler.Handle(command);

            // Assert
            Assert.That(_offer.Status, Is.EqualTo(OfferStatus.Removed));
        }
    }
}
