namespace Eron.DataAccess.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class confproductpropertynames1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProductPropertyNames", "Name", c => c.String(maxLength: 450));
            CreateIndex("dbo.ProductPropertyNames", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.ProductPropertyNames", new[] { "Name" });
            AlterColumn("dbo.ProductPropertyNames", "Name", c => c.String());
        }
    }
}
