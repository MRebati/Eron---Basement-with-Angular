namespace Eron.DataAccess.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class conffileupload2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EronFiles", "Deleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EronFiles", "Deleted");
        }
    }
}
