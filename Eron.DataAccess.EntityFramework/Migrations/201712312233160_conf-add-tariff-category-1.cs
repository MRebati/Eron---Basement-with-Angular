namespace Eron.DataAccess.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class confaddtariffcategory1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CartItems", "ProductId", "dbo.Products");
            DropIndex("dbo.CartItems", new[] { "ProductId" });
            CreateTable(
                "dbo.TariffCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(),
                        Slug = c.String(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Tariffs", "ImageId", c => c.Guid(nullable: false));
            AddColumn("dbo.Tariffs", "TariffCategoryId", c => c.Int(nullable: false));
            AddColumn("dbo.CartItems", "TariffId", c => c.Long());
            AlterColumn("dbo.CartItems", "ProductId", c => c.Long());
            CreateIndex("dbo.Tariffs", "ImageId");
            CreateIndex("dbo.Tariffs", "TariffCategoryId");
            CreateIndex("dbo.CartItems", "ProductId");
            CreateIndex("dbo.CartItems", "TariffId");
            AddForeignKey("dbo.Tariffs", "ImageId", "dbo.EronFiles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Tariffs", "TariffCategoryId", "dbo.TariffCategories", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CartItems", "TariffId", "dbo.Tariffs", "Id");
            AddForeignKey("dbo.CartItems", "ProductId", "dbo.Products", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CartItems", "ProductId", "dbo.Products");
            DropForeignKey("dbo.CartItems", "TariffId", "dbo.Tariffs");
            DropForeignKey("dbo.Tariffs", "TariffCategoryId", "dbo.TariffCategories");
            DropForeignKey("dbo.Tariffs", "ImageId", "dbo.EronFiles");
            DropIndex("dbo.CartItems", new[] { "TariffId" });
            DropIndex("dbo.CartItems", new[] { "ProductId" });
            DropIndex("dbo.Tariffs", new[] { "TariffCategoryId" });
            DropIndex("dbo.Tariffs", new[] { "ImageId" });
            AlterColumn("dbo.CartItems", "ProductId", c => c.Long(nullable: false));
            DropColumn("dbo.CartItems", "TariffId");
            DropColumn("dbo.Tariffs", "TariffCategoryId");
            DropColumn("dbo.Tariffs", "ImageId");
            DropTable("dbo.TariffCategories");
            CreateIndex("dbo.CartItems", "ProductId");
            AddForeignKey("dbo.CartItems", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
        }
    }
}
