namespace Eron.DataAccess.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class conforderupdate201801211 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "OrderNumber", c => c.String(maxLength: 25));
            CreateIndex("dbo.Orders", "OrderNumber", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Orders", new[] { "OrderNumber" });
            AlterColumn("dbo.Orders", "OrderNumber", c => c.String());
        }
    }
}
