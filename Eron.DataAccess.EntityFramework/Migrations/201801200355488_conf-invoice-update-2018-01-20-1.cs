namespace Eron.DataAccess.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class confinvoiceupdate201801201 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Invoices", "InvoiceNumber", c => c.String(nullable: false));
            AlterColumn("dbo.Invoices", "UserId", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Invoices", "UserId", c => c.String());
            AlterColumn("dbo.Invoices", "InvoiceNumber", c => c.String());
        }
    }
}
