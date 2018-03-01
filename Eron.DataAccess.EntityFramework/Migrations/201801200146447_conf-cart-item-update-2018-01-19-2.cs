namespace Eron.DataAccess.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class confcartitemupdate201801192 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CartItems", "UserId", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CartItems", "UserId", c => c.String());
        }
    }
}
