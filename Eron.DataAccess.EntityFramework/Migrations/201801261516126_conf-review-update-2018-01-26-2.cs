namespace Eron.DataAccess.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class confreviewupdate201801262 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reviews", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Reviews", "TariffId", "dbo.Tariffs");
            DropIndex("dbo.Reviews", new[] { "ProductId" });
            DropIndex("dbo.Reviews", new[] { "TariffId" });
            CreateTable(
                "dbo.ServiceReviews",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Username = c.String(),
                        Message = c.String(),
                        Title = c.String(),
                        StarRate = c.Int(nullable: false),
                        ProductId = c.Long(),
                        TariffId = c.Long(),
                        CreateDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .ForeignKey("dbo.Tariffs", t => t.TariffId)
                .Index(t => t.ProductId)
                .Index(t => t.TariffId);
            
            CreateTable(
                "dbo.UserMessages",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Message = c.String(),
                        EmailAddress = c.String(),
                        Seen = c.Boolean(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.Reviews");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.String(),
                        Message = c.String(),
                        Rate = c.Int(nullable: false),
                        ProductId = c.Long(),
                        TariffId = c.Long(),
                        CreateDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.ServiceReviews", "TariffId", "dbo.Tariffs");
            DropForeignKey("dbo.ServiceReviews", "ProductId", "dbo.Products");
            DropIndex("dbo.ServiceReviews", new[] { "TariffId" });
            DropIndex("dbo.ServiceReviews", new[] { "ProductId" });
            DropTable("dbo.UserMessages");
            DropTable("dbo.ServiceReviews");
            CreateIndex("dbo.Reviews", "TariffId");
            CreateIndex("dbo.Reviews", "ProductId");
            AddForeignKey("dbo.Reviews", "TariffId", "dbo.Tariffs", "Id");
            AddForeignKey("dbo.Reviews", "ProductId", "dbo.Products", "Id");
        }
    }
}
