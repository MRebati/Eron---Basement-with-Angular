namespace Eron.DataAccess.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class confinvoiceupdate201801241 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "Amount", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoices", "Amount");
        }
    }
}
