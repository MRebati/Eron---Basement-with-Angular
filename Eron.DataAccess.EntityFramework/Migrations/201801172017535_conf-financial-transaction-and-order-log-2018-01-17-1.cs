namespace Eron.DataAccess.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class conffinancialtransactionandorderlog201801171 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FinanceTransactions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        InvoiceId = c.Long(nullable: false),
                        UserId = c.String(),
                        BankResponse = c.String(),
                        Successful = c.Boolean(nullable: false),
                        ReferenceId = c.String(),
                        CreateDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderLogs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.String(),
                        FromState = c.Int(nullable: false),
                        ToState = c.Int(nullable: false),
                        Description = c.String(),
                        CreateDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Invoices", "ExpireDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Invoices", "Expired", c => c.Boolean(nullable: false));
            AddColumn("dbo.Invoices", "Paid", c => c.Boolean(nullable: false));
            AddColumn("dbo.Invoices", "ReferenceId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoices", "ReferenceId");
            DropColumn("dbo.Invoices", "Paid");
            DropColumn("dbo.Invoices", "Expired");
            DropColumn("dbo.Invoices", "ExpireDateTime");
            DropTable("dbo.OrderLogs");
            DropTable("dbo.FinanceTransactions");
        }
    }
}
