namespace Eron.DataAccess.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class confupdateorder201801161 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "TariffPriceId", "dbo.TariffPrices");
            DropIndex("dbo.Orders", new[] { "TariffPriceId" });
            DropColumn("dbo.Orders", "TariffPriceId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "TariffPriceId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Orders", "TariffPriceId");
            AddForeignKey("dbo.Orders", "TariffPriceId", "dbo.TariffPrices", "Id", cascadeDelete: true);
        }
    }
}
