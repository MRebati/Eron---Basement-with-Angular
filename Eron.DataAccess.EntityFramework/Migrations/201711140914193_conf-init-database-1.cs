namespace Eron.DataAccess.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class confinitdatabase1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InvoiceItems",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Count = c.Int(nullable: false),
                        Description = c.String(),
                        ProductPriceId = c.Guid(nullable: false),
                        TariffPriceId = c.Guid(nullable: false),
                        InvoiceId = c.Long(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Invoices", t => t.InvoiceId, cascadeDelete: true)
                .ForeignKey("dbo.ProductPrices", t => t.ProductPriceId, cascadeDelete: true)
                .ForeignKey("dbo.TariffPrices", t => t.TariffPriceId, cascadeDelete: true)
                .Index(t => t.ProductPriceId)
                .Index(t => t.TariffPriceId)
                .Index(t => t.InvoiceId);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        InvoiceNumber = c.String(),
                        CreateDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductPrices",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IsValid = c.Boolean(nullable: false),
                        Price = c.Long(nullable: false),
                        Description = c.String(),
                        CreateDateTime = c.DateTime(nullable: false),
                        Product_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.Offers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(),
                        MaximumUsage = c.Long(nullable: false),
                        RemainingUsable = c.Long(nullable: false),
                        Percent = c.Int(),
                        Amount = c.Long(),
                        IsValid = c.Boolean(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TariffPrices",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Price = c.Long(nullable: false),
                        IsValid = c.Boolean(nullable: false),
                        TariffId = c.Long(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                        Offer_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tariffs", t => t.TariffId, cascadeDelete: true)
                .ForeignKey("dbo.Offers", t => t.Offer_Id)
                .Index(t => t.TariffId)
                .Index(t => t.Offer_Id);
            
            CreateTable(
                "dbo.Tariffs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TariffName = c.String(),
                        CustomerType = c.Int(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TariffItems",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Name = c.String(),
                        TariffId = c.Long(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tariffs", t => t.TariffId, cascadeDelete: true)
                .Index(t => t.TariffId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        ProductCode = c.String(),
                        ShortDescription = c.String(),
                        LongDescription = c.String(),
                        CreateDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EronFiles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FileName = c.String(),
                        FileUrl = c.String(),
                        FileData = c.Binary(),
                        FileType = c.Int(nullable: false),
                        UploadDateTime = c.DateTime(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                        Product_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.ProductProperties",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Value = c.String(),
                        ProductPropertyNameId = c.Int(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                        Product_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductPropertyNames", t => t.ProductPropertyNameId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .Index(t => t.ProductPropertyNameId)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.ProductPropertyNames",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreateDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OrderNumber = c.String(),
                        Approved = c.Boolean(nullable: false),
                        OrderTypeId = c.Int(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                        Tariff_Id = c.Long(),
                        TariffPrice_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tariffs", t => t.Tariff_Id)
                .ForeignKey("dbo.TariffPrices", t => t.TariffPrice_Id)
                .Index(t => t.Tariff_Id)
                .Index(t => t.TariffPrice_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        PostalCode = c.String(),
                        SocialNumber = c.String(),
                        Address = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Position = c.String(),
                        ImageUrl = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.OfferProductPrices",
                c => new
                    {
                        Offer_Id = c.Long(nullable: false),
                        ProductPrice_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Offer_Id, t.ProductPrice_Id })
                .ForeignKey("dbo.Offers", t => t.Offer_Id, cascadeDelete: true)
                .ForeignKey("dbo.ProductPrices", t => t.ProductPrice_Id, cascadeDelete: true)
                .Index(t => t.Offer_Id)
                .Index(t => t.ProductPrice_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Orders", "TariffPrice_Id", "dbo.TariffPrices");
            DropForeignKey("dbo.Orders", "Tariff_Id", "dbo.Tariffs");
            DropForeignKey("dbo.InvoiceItems", "TariffPriceId", "dbo.TariffPrices");
            DropForeignKey("dbo.InvoiceItems", "ProductPriceId", "dbo.ProductPrices");
            DropForeignKey("dbo.ProductProperties", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.ProductProperties", "ProductPropertyNameId", "dbo.ProductPropertyNames");
            DropForeignKey("dbo.ProductPrices", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.EronFiles", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.TariffPrices", "Offer_Id", "dbo.Offers");
            DropForeignKey("dbo.TariffPrices", "TariffId", "dbo.Tariffs");
            DropForeignKey("dbo.TariffItems", "TariffId", "dbo.Tariffs");
            DropForeignKey("dbo.OfferProductPrices", "ProductPrice_Id", "dbo.ProductPrices");
            DropForeignKey("dbo.OfferProductPrices", "Offer_Id", "dbo.Offers");
            DropForeignKey("dbo.InvoiceItems", "InvoiceId", "dbo.Invoices");
            DropIndex("dbo.OfferProductPrices", new[] { "ProductPrice_Id" });
            DropIndex("dbo.OfferProductPrices", new[] { "Offer_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Orders", new[] { "TariffPrice_Id" });
            DropIndex("dbo.Orders", new[] { "Tariff_Id" });
            DropIndex("dbo.ProductProperties", new[] { "Product_Id" });
            DropIndex("dbo.ProductProperties", new[] { "ProductPropertyNameId" });
            DropIndex("dbo.EronFiles", new[] { "Product_Id" });
            DropIndex("dbo.TariffItems", new[] { "TariffId" });
            DropIndex("dbo.TariffPrices", new[] { "Offer_Id" });
            DropIndex("dbo.TariffPrices", new[] { "TariffId" });
            DropIndex("dbo.ProductPrices", new[] { "Product_Id" });
            DropIndex("dbo.InvoiceItems", new[] { "InvoiceId" });
            DropIndex("dbo.InvoiceItems", new[] { "TariffPriceId" });
            DropIndex("dbo.InvoiceItems", new[] { "ProductPriceId" });
            DropTable("dbo.OfferProductPrices");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Orders");
            DropTable("dbo.ProductPropertyNames");
            DropTable("dbo.ProductProperties");
            DropTable("dbo.EronFiles");
            DropTable("dbo.Products");
            DropTable("dbo.TariffItems");
            DropTable("dbo.Tariffs");
            DropTable("dbo.TariffPrices");
            DropTable("dbo.Offers");
            DropTable("dbo.ProductPrices");
            DropTable("dbo.Invoices");
            DropTable("dbo.InvoiceItems");
        }
    }
}
