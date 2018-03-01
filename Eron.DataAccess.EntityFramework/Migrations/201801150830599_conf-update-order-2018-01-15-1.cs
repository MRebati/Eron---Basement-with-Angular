namespace Eron.DataAccess.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class confupdateorder201801151 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "UserId", c => c.String());
            DropColumn("dbo.Orders", "OrderTypeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "OrderTypeId", c => c.Int(nullable: false));
            DropColumn("dbo.Orders", "UserId");
        }
    }
}
