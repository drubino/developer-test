namespace OrangeBricks.Web.Migrations
{
    using OrangeBricks.Web.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<OrangeBricks.Web.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "OrangeBricks.Web.Models.ApplicationDbContext";
        }

        protected override void Seed(ApplicationDbContext context)
        {
            context.Locations.AddOrUpdate(new Location { Name = "New York", TimeZone = "Eastern Standard Time" });
            context.Locations.AddOrUpdate(new Location { Name = "Chicago", TimeZone = "Central Standard Time" });
            context.Locations.AddOrUpdate(new Location { Name = "Denver", TimeZone = "Mountain Standard Time" });
            context.Locations.AddOrUpdate(new Location { Name = "Los Angeles", TimeZone = "Pacific Standard Time" });
        }
    }
}
