namespace Eron.DataAccess.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class confinvoicelogadd201801261 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InvoiceLogs",
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.InvoiceLogs");
        }
    }
}
