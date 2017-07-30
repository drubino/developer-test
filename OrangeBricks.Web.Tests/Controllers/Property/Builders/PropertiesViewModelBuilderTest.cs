﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using OrangeBricks.Web.Controllers.Property.Builders;
using OrangeBricks.Web.Models;

namespace OrangeBricks.Web.Tests.Controllers.Property.Builders
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
    public class PropertiesViewModelBuilderTest
    {
        private IOrangeBricksContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = Substitute.For<IOrangeBricksContext>();
        }

        [Test]
        public void BuildShouldReturnPropertiesWithMatchingStreetNamesWhenSearchTermIsProvided()
        {
            // Arrange
            var builder = new PropertiesViewModelBuilder(string.Empty, _context);

            var location = new Location { Name = "Test", TimeZone = "Test" };
            var properties = new List<Models.Property>{
                new Models.Property{ StreetName = "Smith Street", Location = location, Description = "", IsListedForSale = true, Offers = new Offer[0], Viewings = new Viewing[0]},
                new Models.Property{ StreetName = "Jones Street", Location = location, Description = "", IsListedForSale = true, Offers = new Offer[0], Viewings = new Viewing[0]}
            };

            var mockSet = Substitute.For<IDbSet<Models.Property>>()
                .Initialize(properties.AsQueryable());

            _context.Properties.Returns(mockSet);

            var query = new PropertiesQuery
            {
                Search = "Smith Street"
            };

            // Act
            var viewModel = builder.Build(query);

            // Assert
            Assert.That(viewModel.Properties.Count, Is.EqualTo(1));
        }

        [Test]
        public void BuildShouldReturnPropertiesWithMatchingDescriptionsWhenSearchTermIsProvided()
        {
            // Arrange
            var builder = new PropertiesViewModelBuilder(string.Empty, _context);

            var location = new Location { Name = "", TimeZone = "" };
            var properties = new List<Models.Property>{
                new Models.Property{ StreetName = "", Location = location, Description = "Great location", IsListedForSale = true, Offers = new Offer[0], Viewings = new Viewing[0]},
                new Models.Property{ StreetName = "", Location = location, Description = "Town house", IsListedForSale = true, Offers = new Offer[0], Viewings = new Viewing[0]}
            };

            var mockSet = Substitute.For<IDbSet<Models.Property>>()
                .Initialize(properties.AsQueryable());

            _context.Properties.Returns(mockSet);

            var query = new PropertiesQuery
            {
                Search = "Town"
            };

            // Act
            var viewModel = builder.Build(query);

            // Assert
            Assert.That(viewModel.Properties.Count, Is.EqualTo(1));
            Assert.That(viewModel.Properties.All(p => p.Description.Contains("Town")));
        }
    }
}
