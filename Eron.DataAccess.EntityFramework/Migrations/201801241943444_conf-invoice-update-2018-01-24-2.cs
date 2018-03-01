namespace Eron.DataAccess.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class confinvoiceupdate201801242 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Invoices", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Invoices", "UserId");
            AddForeignKey("dbo.Invoices", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Invoices", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Invoices", new[] { "UserId" });
            AlterColumn("dbo.Invoices", "UserId", c => c.String(nullable: false));
        }
    }
}
