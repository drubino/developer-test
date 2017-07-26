namespace OrangeBricks.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserToOffers : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Offers", "Property_Id", "dbo.Properties");
            DropIndex("dbo.Offers", new[] { "Property_Id" });
            RenameColumn(table: "dbo.Offers", name: "Property_Id", newName: "PropertyId");
            AddColumn("dbo.Offers", "Username", c => c.String(maxLength: 100));
            AlterColumn("dbo.Offers", "PropertyId", c => c.Int(nullable: false));
            CreateIndex("dbo.Offers", new[] { "Username", "PropertyId" }, unique: true, name: "UX_Username_PropertyId");
            AddForeignKey("dbo.Offers", "PropertyId", "dbo.Properties", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Offers", "PropertyId", "dbo.Properties");
            DropIndex("dbo.Offers", "UX_Username_PropertyId");
            AlterColumn("dbo.Offers", "PropertyId", c => c.Int());
            DropColumn("dbo.Offers", "Username");
            RenameColumn(table: "dbo.Offers", name: "PropertyId", newName: "Property_Id");
            CreateIndex("dbo.Offers", "Property_Id");
            AddForeignKey("dbo.Offers", "Property_Id", "dbo.Properties", "Id");
        }
    }
}
