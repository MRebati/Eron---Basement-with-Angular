namespace Eron.DataAccess.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class confupdatetariffcategory20180104 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TariffCategories", "Promoted", c => c.Boolean(nullable: false));
            AddColumn("dbo.TariffCategories", "ViewOnHomePage", c => c.Boolean(nullable: false));
            AddColumn("dbo.TariffCategories", "ParentId", c => c.Int());
            CreateIndex("dbo.TariffCategories", "ParentId");
            AddForeignKey("dbo.TariffCategories", "ParentId", "dbo.TariffCategories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TariffCategories", "ParentId", "dbo.TariffCategories");
            DropIndex("dbo.TariffCategories", new[] { "ParentId" });
            DropColumn("dbo.TariffCategories", "ParentId");
            DropColumn("dbo.TariffCategories", "ViewOnHomePage");
            DropColumn("dbo.TariffCategories", "Promoted");
        }
    }
}
