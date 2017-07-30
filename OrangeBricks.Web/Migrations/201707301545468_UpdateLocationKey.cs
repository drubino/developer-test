namespace OrangeBricks.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateLocationKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Properties", "LocationId", "dbo.Locations");
            DropIndex("dbo.Properties", new[] { "LocationId" });
            RenameColumn(table: "dbo.Properties", name: "LocationId", newName: "LocationName");
            DropPrimaryKey("dbo.Locations");
            AlterColumn("dbo.Locations", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Locations", "TimeZone", c => c.String(maxLength: 100));
            AlterColumn("dbo.Properties", "LocationName", c => c.String(nullable: false, maxLength: 100));
            AddPrimaryKey("dbo.Locations", "Name");
            CreateIndex("dbo.Properties", "LocationName");
            AddForeignKey("dbo.Properties", "LocationName", "dbo.Locations", "Name", cascadeDelete: true);
            DropColumn("dbo.Locations", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Locations", "Id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Properties", "LocationName", "dbo.Locations");
            DropIndex("dbo.Properties", new[] { "LocationName" });
            DropPrimaryKey("dbo.Locations");
            AlterColumn("dbo.Properties", "LocationName", c => c.Int(nullable: false));
            AlterColumn("dbo.Locations", "TimeZone", c => c.String());
            AlterColumn("dbo.Locations", "Name", c => c.String());
            AddPrimaryKey("dbo.Locations", "Id");
            RenameColumn(table: "dbo.Properties", name: "LocationName", newName: "LocationId");
            CreateIndex("dbo.Properties", "LocationId");
            AddForeignKey("dbo.Properties", "LocationId", "dbo.Locations", "Id", cascadeDelete: true);
        }
    }
}
