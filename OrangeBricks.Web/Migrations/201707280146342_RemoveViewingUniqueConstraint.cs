namespace OrangeBricks.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveViewingUniqueConstraint : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Viewings", "UX_Username_PropertyId");
            CreateIndex("dbo.Viewings", "PropertyId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Viewings", new[] { "PropertyId" });
            CreateIndex("dbo.Viewings", new[] { "Username", "PropertyId" }, unique: true, name: "UX_Username_PropertyId");
        }
    }
}
