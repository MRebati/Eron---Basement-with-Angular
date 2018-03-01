namespace Eron.DataAccess.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class confupdatetariffcategory201801042 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tariffs", "ImageId", "dbo.EronFiles");
            DropIndex("dbo.Tariffs", new[] { "ImageId" });
            AddColumn("dbo.TariffCategories", "ImageId", c => c.Guid());
            AlterColumn("dbo.Tariffs", "ImageId", c => c.Guid());
            CreateIndex("dbo.Tariffs", "ImageId");
            CreateIndex("dbo.TariffCategories", "ImageId");
            AddForeignKey("dbo.TariffCategories", "ImageId", "dbo.EronFiles", "Id");
            AddForeignKey("dbo.Tariffs", "ImageId", "dbo.EronFiles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tariffs", "ImageId", "dbo.EronFiles");
            DropForeignKey("dbo.TariffCategories", "ImageId", "dbo.EronFiles");
            DropIndex("dbo.TariffCategories", new[] { "ImageId" });
            DropIndex("dbo.Tariffs", new[] { "ImageId" });
            AlterColumn("dbo.Tariffs", "ImageId", c => c.Guid(nullable: false));
            DropColumn("dbo.TariffCategories", "ImageId");
            CreateIndex("dbo.Tariffs", "ImageId");
            AddForeignKey("dbo.Tariffs", "ImageId", "dbo.EronFiles", "Id", cascadeDelete: true);
        }
    }
}
