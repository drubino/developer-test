namespace OrangeBricks.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveOfferUniqueConstraint : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Offers", "UX_Username_PropertyId");
            CreateIndex("dbo.Offers", "PropertyId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Offers", new[] { "PropertyId" });
            CreateIndex("dbo.Offers", new[] { "Username", "PropertyId" }, unique: true, name: "UX_Username_PropertyId");
        }
    }
}
