namespace Eron.DataAccess.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class confusermessageupdate201802031 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserMessages", "FirstName", c => c.String());
            AddColumn("dbo.UserMessages", "LastName", c => c.String());
            AddColumn("dbo.UserMessages", "Title", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserMessages", "Title");
            DropColumn("dbo.UserMessages", "LastName");
            DropColumn("dbo.UserMessages", "FirstName");
        }
    }
}
