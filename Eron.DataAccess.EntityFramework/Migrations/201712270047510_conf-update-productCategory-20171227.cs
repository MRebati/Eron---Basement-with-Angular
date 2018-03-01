namespace Eron.DataAccess.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class confupdateproductCategory20171227 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductCategories", "Promoted", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductCategories", "ViewOnHomePage", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductCategories", "ViewOnHomePage");
            DropColumn("dbo.ProductCategories", "Promoted");
        }
    }
}
