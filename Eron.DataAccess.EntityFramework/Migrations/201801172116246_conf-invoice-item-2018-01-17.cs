namespace Eron.DataAccess.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class confinvoiceitem20180117 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InvoiceItems", "CartItemId", c => c.Long());
            CreateIndex("dbo.InvoiceItems", "CartItemId");
            AddForeignKey("dbo.InvoiceItems", "CartItemId", "dbo.CartItems", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InvoiceItems", "CartItemId", "dbo.CartItems");
            DropIndex("dbo.InvoiceItems", new[] { "CartItemId" });
            DropColumn("dbo.InvoiceItems", "CartItemId");
        }
    }
}
