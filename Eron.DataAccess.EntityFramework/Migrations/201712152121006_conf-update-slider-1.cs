namespace Eron.DataAccess.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class confupdateslider1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.BannerSliders", new[] { "GroupName" });
            CreateIndex("dbo.BannerSliders", "GroupName");
        }
        
        public override void Down()
        {
            DropIndex("dbo.BannerSliders", new[] { "GroupName" });
            CreateIndex("dbo.BannerSliders", "GroupName", unique: true);
        }
    }
}
