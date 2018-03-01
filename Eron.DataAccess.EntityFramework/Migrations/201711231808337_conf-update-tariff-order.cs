namespace Eron.DataAccess.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class confupdatetarifforder : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "Tariff_Id", "dbo.Tariffs");
            DropForeignKey("dbo.Orders", "TariffPrice_Id", "dbo.TariffPrices");
            DropIndex("dbo.Orders", new[] { "Tariff_Id" });
            DropIndex("dbo.Orders", new[] { "TariffPrice_Id" });
            RenameColumn(table: "dbo.Orders", name: "Tariff_Id", newName: "TariffId");
            RenameColumn(table: "dbo.Orders", name: "TariffPrice_Id", newName: "TariffPriceId");
            AddColumn("dbo.Orders", "Count", c => c.Int(nullable: false));
            AlterColumn("dbo.Tariffs", "TariffName", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "TariffId", c => c.Long(nullable: false));
            AlterColumn("dbo.Orders", "TariffPriceId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Orders", "TariffId");
            CreateIndex("dbo.Orders", "TariffPriceId");
            AddForeignKey("dbo.Orders", "TariffId", "dbo.Tariffs", "Id", cascadeDelete: false);
            AddForeignKey("dbo.Orders", "TariffPriceId", "dbo.TariffPrices", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "TariffPriceId", "dbo.TariffPrices");
            DropForeignKey("dbo.Orders", "TariffId", "dbo.Tariffs");
            DropIndex("dbo.Orders", new[] { "TariffPriceId" });
            DropIndex("dbo.Orders", new[] { "TariffId" });
            AlterColumn("dbo.Orders", "TariffPriceId", c => c.Guid());
            AlterColumn("dbo.Orders", "TariffId", c => c.Long());
            AlterColumn("dbo.Tariffs", "TariffName", c => c.String());
            DropColumn("dbo.Orders", "Count");
            RenameColumn(table: "dbo.Orders", name: "TariffPriceId", newName: "TariffPrice_Id");
            RenameColumn(table: "dbo.Orders", name: "TariffId", newName: "Tariff_Id");
            CreateIndex("dbo.Orders", "TariffPrice_Id");
            CreateIndex("dbo.Orders", "Tariff_Id");
            AddForeignKey("dbo.Orders", "TariffPrice_Id", "dbo.TariffPrices", "Id");
            AddForeignKey("dbo.Orders", "Tariff_Id", "dbo.Tariffs", "Id");
        }
    }
}
