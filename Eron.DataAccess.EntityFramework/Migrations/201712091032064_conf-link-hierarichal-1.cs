namespace Eron.DataAccess.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class conflinkhierarichal1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Links",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LinkType = c.Int(nullable: false),
                        LinkPlacement = c.Int(nullable: false),
                        Url = c.String(),
                        LinkText = c.String(),
                        Target = c.Int(nullable: false),
                        Image = c.String(),
                        IconClass = c.String(),
                        ParentId = c.Int(),
                        CreateDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Links", t => t.ParentId)
                .Index(t => t.ParentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Links", "ParentId", "dbo.Links");
            DropIndex("dbo.Links", new[] { "ParentId" });
            DropTable("dbo.Links");
        }
    }
}
