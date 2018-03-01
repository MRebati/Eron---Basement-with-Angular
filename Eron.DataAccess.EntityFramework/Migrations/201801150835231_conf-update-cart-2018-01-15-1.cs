namespace Eron.DataAccess.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class confupdatecart201801151 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CartItems", "OrderId", c => c.Guid());
            CreateIndex("dbo.CartItems", "OrderId");
            AddForeignKey("dbo.CartItems", "OrderId", "dbo.Orders", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CartItems", "OrderId", "dbo.Orders");
            DropIndex("dbo.CartItems", new[] { "OrderId" });
            DropColumn("dbo.CartItems", "OrderId");
        }
    }
}
