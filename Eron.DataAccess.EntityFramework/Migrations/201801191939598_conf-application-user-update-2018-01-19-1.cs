namespace Eron.DataAccess.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class confapplicationuserupdate201801191 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ImageId", c => c.String());
            AddColumn("dbo.AspNetUsers", "CompanyName", c => c.String());
            AddColumn("dbo.AspNetUsers", "CityName", c => c.String());
            AddColumn("dbo.AspNetUsers", "ProvinceName", c => c.String());
            AddColumn("dbo.AspNetUsers", "FaxNumber", c => c.String());
            DropColumn("dbo.AspNetUsers", "ImageUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "ImageUrl", c => c.String());
            DropColumn("dbo.AspNetUsers", "FaxNumber");
            DropColumn("dbo.AspNetUsers", "ProvinceName");
            DropColumn("dbo.AspNetUsers", "CityName");
            DropColumn("dbo.AspNetUsers", "CompanyName");
            DropColumn("dbo.AspNetUsers", "ImageId");
        }
    }
}
