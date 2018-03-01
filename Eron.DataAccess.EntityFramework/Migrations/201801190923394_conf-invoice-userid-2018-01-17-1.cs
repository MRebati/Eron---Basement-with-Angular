namespace Eron.DataAccess.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class confinvoiceuserid201801171 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoices", "UserId");
        }
    }
}
