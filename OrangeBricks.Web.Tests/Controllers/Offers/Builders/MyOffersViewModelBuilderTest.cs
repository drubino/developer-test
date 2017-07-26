using NSubstitute;
using NUnit.Framework;
using OrangeBricks.Web.Controllers.Offers.Builders;
using OrangeBricks.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrangeBricks.Web.Tests.Controllers.Offers.Builders
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
        public void BuildShouldReturnOffersForTheCurrentUser()
        {
            var username = "user";
            var builder = new MyOffersViewModelBuilder(username, _context);

            var properties = new List<Models.Property>{
                new Models.Property{ Id = 0, StreetName = "Smith Street", Description = "", IsListedForSale = true },
                new Models.Property{ Id = 1, StreetName = "Jones Street", Description = "", IsListedForSale = true }
            };

            var offers = new List<Offer>
            {
                new Offer { Username = "", Property = properties[0] },
                new Offer { Username = username, Property = properties[0] }, //Should be returned
                new Offer { Username = "", Property = properties[1] },
            };

            var offersMockSet = Substitute.For<IDbSet<Offer>>()
                .Initialize(offers.AsQueryable());

            _context.Offers.Returns(offersMockSet);

            var viewModel = builder.Build();
            Assert.That(viewModel.OffersOnProperty.Count, Is.EqualTo(1));
            Assert.That(viewModel.OffersOnProperty.First().PropertyId, Is.EqualTo(0));
        }
    }
}
