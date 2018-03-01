namespace Eron.DataAccess.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class confupdateeronfile201801151 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EronFiles", "OrderId", c => c.Guid());
            CreateIndex("dbo.EronFiles", "OrderId");
            AddForeignKey("dbo.EronFiles", "OrderId", "dbo.Orders", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EronFiles", "OrderId", "dbo.Orders");
            DropIndex("dbo.EronFiles", new[] { "OrderId" });
            DropColumn("dbo.EronFiles", "OrderId");
        }
    }
}
