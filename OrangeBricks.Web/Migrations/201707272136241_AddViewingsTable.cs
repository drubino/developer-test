namespace OrangeBricks.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddViewingsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Viewings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(maxLength: 100),
                        PropertyId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Properties", t => t.PropertyId, cascadeDelete: true)
                .Index(t => new { t.Username, t.PropertyId }, unique: true, name: "UX_Username_PropertyId");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Viewings", "PropertyId", "dbo.Properties");
            DropIndex("dbo.Viewings", "UX_Username_PropertyId");
            DropTable("dbo.Viewings");
        }
    }
}
