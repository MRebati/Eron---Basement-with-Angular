namespace Eron.DataAccess.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class confproductcategoryupdate201802161 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductCategories", "Slug", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductCategories", "Slug");
        }
    }
}
