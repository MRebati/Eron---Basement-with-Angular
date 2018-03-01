namespace Eron.DataAccess.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class confupdateorder201801152 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "HasDesignOrder", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orders", "DesignPrice", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "DesignPrice");
            DropColumn("dbo.Orders", "HasDesignOrder");
        }
    }
}
