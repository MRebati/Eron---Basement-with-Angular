namespace Eron.DataAccess.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class confinvoiceupdate201802021 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "Type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoices", "Type");
        }
    }
}
