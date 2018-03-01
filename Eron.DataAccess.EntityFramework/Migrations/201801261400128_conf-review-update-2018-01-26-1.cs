namespace Eron.DataAccess.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class confreviewupdate201801261 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reviews", "ProductId", c => c.Long());
            AddColumn("dbo.Reviews", "TariffId", c => c.Long());
            CreateIndex("dbo.Reviews", "ProductId");
            CreateIndex("dbo.Reviews", "TariffId");
            AddForeignKey("dbo.Reviews", "ProductId", "dbo.Products", "Id");
            AddForeignKey("dbo.Reviews", "TariffId", "dbo.Tariffs", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "TariffId", "dbo.Tariffs");
            DropForeignKey("dbo.Reviews", "ProductId", "dbo.Products");
            DropIndex("dbo.Reviews", new[] { "TariffId" });
            DropIndex("dbo.Reviews", new[] { "ProductId" });
            DropColumn("dbo.Reviews", "TariffId");
            DropColumn("dbo.Reviews", "ProductId");
        }
    }
}
