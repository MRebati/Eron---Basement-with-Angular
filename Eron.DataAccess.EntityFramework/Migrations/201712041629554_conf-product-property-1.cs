namespace Eron.DataAccess.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class confproductproperty1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductPrices", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.ProductProperties", "Product_Id", "dbo.Products");
            DropIndex("dbo.ProductPrices", new[] { "Product_Id" });
            DropIndex("dbo.ProductProperties", new[] { "Product_Id" });
            RenameColumn(table: "dbo.ProductPrices", name: "Product_Id", newName: "ProductId");
            RenameColumn(table: "dbo.ProductProperties", name: "Product_Id", newName: "ProductId");
            AlterColumn("dbo.ProductPrices", "ProductId", c => c.Long(nullable: false));
            AlterColumn("dbo.ProductProperties", "ProductId", c => c.Long(nullable: false));
            CreateIndex("dbo.ProductPrices", "ProductId");
            CreateIndex("dbo.ProductProperties", "ProductId");
            AddForeignKey("dbo.ProductPrices", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProductProperties", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductProperties", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductPrices", "ProductId", "dbo.Products");
            DropIndex("dbo.ProductProperties", new[] { "ProductId" });
            DropIndex("dbo.ProductPrices", new[] { "ProductId" });
            AlterColumn("dbo.ProductProperties", "ProductId", c => c.Long());
            AlterColumn("dbo.ProductPrices", "ProductId", c => c.Long());
            RenameColumn(table: "dbo.ProductProperties", name: "ProductId", newName: "Product_Id");
            RenameColumn(table: "dbo.ProductPrices", name: "ProductId", newName: "Product_Id");
            CreateIndex("dbo.ProductProperties", "Product_Id");
            CreateIndex("dbo.ProductPrices", "Product_Id");
            AddForeignKey("dbo.ProductProperties", "Product_Id", "dbo.Products", "Id");
            AddForeignKey("dbo.ProductPrices", "Product_Id", "dbo.Products", "Id");
        }
    }
}
