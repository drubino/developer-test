using NSubstitute;
using NUnit.Framework;
using OrangeBricks.Web.Controllers.Viewings.Builders;
using OrangeBricks.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrangeBricks.Web.Tests.Controllers.Viewings.Builders
{
    public static class ExtentionMethods
    {
        public static IDbSet<T> Initialize<T>(this IDbSet<T> dbSet, IQueryable<T> data) where T : class
        {
            dbSet.Provider.Returns(data.Provider);
            dbSet.Expression.Returns(data.Expression);
            dbSet.ElementType.Returns(data.ElementType);
            dbSet.GetEnumerator().Returns(data.GetEnumerator());
            return dbSet;
        }
    }

    [TestFixture]
    public class MyOffersViewModelBuilderTest
    {
        private IOrangeBricksContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = Substitute.For<IOrangeBricksContext>();
        }

        [Test]
        public void BuildShouldReturnViewingsForTheCurrentUser()
        {
            var username = "user";
            var builder = new MyViewingsViewModelBuilder(username, _context);

            var location = new Location { Name = "Test", TimeZone = "Test" };
            var properties = new List<Models.Property>{
                new Models.Property{ Id = 0, StreetName = "Smith Street", Location = location, Description = "", IsListedForSale = true },
                new Models.Property{ Id = 1, StreetName = "Jones Street", Location = location, Description = "", IsListedForSale = true }
            };

            var returnedProperty = properties[0];
            var viewings = new List<Viewing>
            {
                new Viewing { Username = "", Property = properties[1] },
                new Viewing { Username = username, Property = returnedProperty }, //Should be returned
                new Viewing { Username = "", Property = properties[1] },
            };

            var offersMockSet = Substitute.For<IDbSet<Offer>>()
                .Initialize(new Offer[0].AsQueryable());

            var viewingsMockSet = Substitute.For<IDbSet<Viewing>>()
                .Initialize(viewings.AsQueryable());

            _context.Offers.Returns(offersMockSet);
            _context.Viewings.Returns(viewingsMockSet);

            var viewModel = builder.Build();
            Assert.That(viewModel.ViewingsForProperty.Count, Is.EqualTo(1));
            Assert.That(viewModel.ViewingsForProperty.First().PropertyId, Is.EqualTo(returnedProperty.Id));
        }

        [Test]
        public void BuildShouldReturnFormattedDates()
        {
            var username = "user";
            var builder = new MyViewingsViewModelBuilder(username, _context);

            var date = new DateTime(2000, 1, 1, 13, 0, 0);
            var formattedDate = "Saturday, January 1, 2000";
            var formattedTime = "1:00 PM EST";

            var location = new Location { Name = "Test", TimeZone = "Eastern Standard Time" };
            var viewings = new List<Viewing>
            {
                new Viewing { Username = username, Date = date, Property = new Models.Property { Location = location } },
            };

            var viewingsMockSet = Substitute.For<IDbSet<Viewing>>()
                .Initialize(viewings.AsQueryable());

            _context.Viewings.Returns(viewingsMockSet);

            var viewModel = builder.Build();
            var viewingViewModel = viewModel.ViewingsForProperty.First().Viewings.First();
            Assert.That(viewingViewModel.ViewingDate, Is.EqualTo(formattedDate));
            Assert.That(viewingViewModel.ViewingTime, Is.EqualTo(formattedTime));
        }
    }
}
