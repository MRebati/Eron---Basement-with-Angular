namespace Eron.DataAccess.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class confinvoiceupdate201801231 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "InvoiceStatus", c => c.Int(nullable: false));
            AddColumn("dbo.Invoices", "Progress", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoices", "Progress");
            DropColumn("dbo.Invoices", "InvoiceStatus");
        }
    }
}
