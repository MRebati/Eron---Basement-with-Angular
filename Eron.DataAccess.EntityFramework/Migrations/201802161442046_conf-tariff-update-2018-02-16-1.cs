namespace Eron.DataAccess.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class conftariffupdate201802161 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tariffs", "UnitType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tariffs", "UnitType");
        }
    }
}
