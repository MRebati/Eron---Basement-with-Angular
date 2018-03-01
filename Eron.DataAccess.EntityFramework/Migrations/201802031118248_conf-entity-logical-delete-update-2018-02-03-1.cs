namespace Eron.DataAccess.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class confentitylogicaldeleteupdate201802031 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BannerSliders", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.EronFiles", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orders", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Tariffs", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.TariffCategories", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.TariffItems", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.TariffPrices", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductCategories", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductPrices", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Offers", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductProperties", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductPropertyNames", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.CartItems", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.FinanceTransactions", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Invoices", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.InvoiceItems", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.InvoiceLogs", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Links", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.OrderLogs", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Pages", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.ServiceReviews", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserMessages", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.WishListItems", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WishListItems", "IsDeleted");
            DropColumn("dbo.UserMessages", "IsDeleted");
            DropColumn("dbo.ServiceReviews", "IsDeleted");
            DropColumn("dbo.Pages", "IsDeleted");
            DropColumn("dbo.OrderLogs", "IsDeleted");
            DropColumn("dbo.Links", "IsDeleted");
            DropColumn("dbo.InvoiceLogs", "IsDeleted");
            DropColumn("dbo.InvoiceItems", "IsDeleted");
            DropColumn("dbo.Invoices", "IsDeleted");
            DropColumn("dbo.FinanceTransactions", "IsDeleted");
            DropColumn("dbo.CartItems", "IsDeleted");
            DropColumn("dbo.ProductPropertyNames", "IsDeleted");
            DropColumn("dbo.ProductProperties", "IsDeleted");
            DropColumn("dbo.Offers", "IsDeleted");
            DropColumn("dbo.ProductPrices", "IsDeleted");
            DropColumn("dbo.ProductCategories", "IsDeleted");
            DropColumn("dbo.Products", "IsDeleted");
            DropColumn("dbo.TariffPrices", "IsDeleted");
            DropColumn("dbo.TariffItems", "IsDeleted");
            DropColumn("dbo.TariffCategories", "IsDeleted");
            DropColumn("dbo.Tariffs", "IsDeleted");
            DropColumn("dbo.Orders", "IsDeleted");
            DropColumn("dbo.EronFiles", "IsDeleted");
            DropColumn("dbo.BannerSliders", "IsDeleted");
        }
    }
}
