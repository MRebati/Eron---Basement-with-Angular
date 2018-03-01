namespace Eron.DataAccess.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class conflinkpriority1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Keywords = c.String(),
                        ParentId = c.Int(),
                        CreateDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductCategories", t => t.ParentId)
                .Index(t => t.ParentId);
            
            AddColumn("dbo.Products", "ProductCategory_Id", c => c.Int());
            AddColumn("dbo.Links", "Priority", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "ProductCategory_Id");
            AddForeignKey("dbo.Products", "ProductCategory_Id", "dbo.ProductCategories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "ProductCategory_Id", "dbo.ProductCategories");
            DropForeignKey("dbo.ProductCategories", "ParentId", "dbo.ProductCategories");
            DropIndex("dbo.ProductCategories", new[] { "ParentId" });
            DropIndex("dbo.Products", new[] { "ProductCategory_Id" });
            DropColumn("dbo.Links", "Priority");
            DropColumn("dbo.Products", "ProductCategory_Id");
            DropTable("dbo.ProductCategories");
        }
    }
}
