namespace Eron.DataAccess.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class confupdatetariffcategory1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TariffCategories", "Keywords", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TariffCategories", "Keywords");
        }
    }
}
