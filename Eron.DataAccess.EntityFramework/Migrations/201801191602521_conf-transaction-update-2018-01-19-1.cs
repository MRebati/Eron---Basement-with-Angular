namespace Eron.DataAccess.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class conftransactionupdate201801191 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FinanceTransactions", "TransactionNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FinanceTransactions", "TransactionNumber");
        }
    }
}
