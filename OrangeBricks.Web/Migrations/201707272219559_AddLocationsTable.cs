namespace OrangeBricks.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLocationsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        TimeZone = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Properties", "LocationId", c => c.Int(nullable: false));
            CreateIndex("dbo.Properties", "LocationId");
            AddForeignKey("dbo.Properties", "LocationId", "dbo.Locations", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Properties", "LocationId", "dbo.Locations");
            DropIndex("dbo.Properties", new[] { "LocationId" });
            DropColumn("dbo.Properties", "LocationId");
            DropTable("dbo.Locations");
        }
    }
}
