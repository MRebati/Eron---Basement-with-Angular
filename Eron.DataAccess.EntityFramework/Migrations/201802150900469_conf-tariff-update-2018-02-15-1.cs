namespace Eron.DataAccess.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class conftariffupdate201802151 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tariffs", "DesignPrice", c => c.Long());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tariffs", "DesignPrice");
        }
    }
}
