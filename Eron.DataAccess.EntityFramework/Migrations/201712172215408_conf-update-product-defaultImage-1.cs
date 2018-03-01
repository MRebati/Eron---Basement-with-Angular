namespace Eron.DataAccess.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class confupdateproductdefaultImage1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "DefaultImage", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "DefaultImage");
        }
    }
}
