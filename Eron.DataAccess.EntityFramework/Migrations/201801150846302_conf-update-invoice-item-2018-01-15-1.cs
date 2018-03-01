namespace Eron.DataAccess.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class confupdateinvoiceitem201801151 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.InvoiceItems", "ProductPriceId", "dbo.ProductPrices");
            DropForeignKey("dbo.InvoiceItems", "TariffPriceId", "dbo.TariffPrices");
            DropIndex("dbo.InvoiceItems", new[] { "ProductPriceId" });
            DropIndex("dbo.InvoiceItems", new[] { "TariffPriceId" });
            AddColumn("dbo.InvoiceItems", "OrderId", c => c.Guid());
            AlterColumn("dbo.InvoiceItems", "ProductPriceId", c => c.Guid());
            AlterColumn("dbo.InvoiceItems", "TariffPriceId", c => c.Guid());
            CreateIndex("dbo.InvoiceItems", "OrderId");
            CreateIndex("dbo.InvoiceItems", "ProductPriceId");
            CreateIndex("dbo.InvoiceItems", "TariffPriceId");
            AddForeignKey("dbo.InvoiceItems", "OrderId", "dbo.Orders", "Id");
            AddForeignKey("dbo.InvoiceItems", "ProductPriceId", "dbo.ProductPrices", "Id");
            AddForeignKey("dbo.InvoiceItems", "TariffPriceId", "dbo.TariffPrices", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InvoiceItems", "TariffPriceId", "dbo.TariffPrices");
            DropForeignKey("dbo.InvoiceItems", "ProductPriceId", "dbo.ProductPrices");
            DropForeignKey("dbo.InvoiceItems", "OrderId", "dbo.Orders");
            DropIndex("dbo.InvoiceItems", new[] { "TariffPriceId" });
            DropIndex("dbo.InvoiceItems", new[] { "ProductPriceId" });
            DropIndex("dbo.InvoiceItems", new[] { "OrderId" });
            AlterColumn("dbo.InvoiceItems", "TariffPriceId", c => c.Guid(nullable: false));
            AlterColumn("dbo.InvoiceItems", "ProductPriceId", c => c.Guid(nullable: false));
            DropColumn("dbo.InvoiceItems", "OrderId");
            CreateIndex("dbo.InvoiceItems", "TariffPriceId");
            CreateIndex("dbo.InvoiceItems", "ProductPriceId");
            AddForeignKey("dbo.InvoiceItems", "TariffPriceId", "dbo.TariffPrices", "Id", cascadeDelete: true);
            AddForeignKey("dbo.InvoiceItems", "ProductPriceId", "dbo.ProductPrices", "Id", cascadeDelete: true);
        }
    }
}
