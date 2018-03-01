namespace Eron.DataAccess.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class confaddbannerslider : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BannerSliders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupName = c.String(maxLength: 50),
                        FileId = c.Guid(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                        LinkUrl = c.String(),
                        IsSlider = c.Boolean(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EronFiles", t => t.FileId, cascadeDelete: true)
                .Index(t => t.GroupName, unique: true)
                .Index(t => t.FileId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BannerSliders", "FileId", "dbo.EronFiles");
            DropIndex("dbo.BannerSliders", new[] { "FileId" });
            DropIndex("dbo.BannerSliders", new[] { "GroupName" });
            DropTable("dbo.BannerSliders");
        }
    }
}
