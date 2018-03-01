namespace Eron.DataAccess.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class conffileupload3 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.EronFiles", name: "Product_Id", newName: "ProductId");
            RenameIndex(table: "dbo.EronFiles", name: "IX_Product_Id", newName: "IX_ProductId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.EronFiles", name: "IX_ProductId", newName: "IX_Product_Id");
            RenameColumn(table: "dbo.EronFiles", name: "ProductId", newName: "Product_Id");
        }
    }
}
