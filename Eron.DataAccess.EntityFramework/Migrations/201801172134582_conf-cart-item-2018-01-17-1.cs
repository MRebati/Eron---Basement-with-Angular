namespace Eron.DataAccess.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class confcartitem201801171 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CartItems", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.CartItems", "TariffId", "dbo.Tariffs");
            DropForeignKey("dbo.CartItems", "ProductId", "dbo.Products");
            DropIndex("dbo.CartItems", new[] { "ProductId" });
            DropIndex("dbo.CartItems", new[] { "TariffId" });
            DropIndex("dbo.CartItems", new[] { "OrderId" });
            AlterColumn("dbo.CartItems", "ProductId", c => c.Long(nullable: false));
            CreateIndex("dbo.CartItems", "ProductId");
            AddForeignKey("dbo.CartItems", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
            DropColumn("dbo.CartItems", "TariffId");
            DropColumn("dbo.CartItems", "OrderId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CartItems", "OrderId", c => c.Guid());
            AddColumn("dbo.CartItems", "TariffId", c => c.Long());
            DropForeignKey("dbo.CartItems", "ProductId", "dbo.Products");
            DropIndex("dbo.CartItems", new[] { "ProductId" });
            AlterColumn("dbo.CartItems", "ProductId", c => c.Long());
            CreateIndex("dbo.CartItems", "OrderId");
            CreateIndex("dbo.CartItems", "TariffId");
            CreateIndex("dbo.CartItems", "ProductId");
            AddForeignKey("dbo.CartItems", "ProductId", "dbo.Products", "Id");
            AddForeignKey("dbo.CartItems", "TariffId", "dbo.Tariffs", "Id");
            AddForeignKey("dbo.CartItems", "OrderId", "dbo.Orders", "Id");
        }
    }
}
