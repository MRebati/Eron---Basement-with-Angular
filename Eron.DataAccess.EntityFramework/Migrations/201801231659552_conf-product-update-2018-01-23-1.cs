namespace Eron.DataAccess.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class confproductupdate201801231 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ExistsInShop", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ExistsInShop");
        }
    }
}
