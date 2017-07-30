namespace OrangeBricks.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SwitchToUserId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Offers", "UserId", c => c.String(nullable: false));
            AddColumn("dbo.Viewings", "UserId", c => c.String(nullable: false));
            DropColumn("dbo.Offers", "Username");
            DropColumn("dbo.Viewings", "Username");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Viewings", "Username", c => c.String(maxLength: 100));
            AddColumn("dbo.Offers", "Username", c => c.String(maxLength: 100));
            DropColumn("dbo.Viewings", "UserId");
            DropColumn("dbo.Offers", "UserId");
        }
    }
}
