namespace Eron.DataAccess.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class conffinancetransactionupdate201801241 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.FinanceTransactions", "InvoiceId");
            AddForeignKey("dbo.FinanceTransactions", "InvoiceId", "dbo.Invoices", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FinanceTransactions", "InvoiceId", "dbo.Invoices");
            DropIndex("dbo.FinanceTransactions", new[] { "InvoiceId" });
        }
    }
}
