namespace Inventory.LunarMed.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Client",
                c => new
                    {
                        ClientId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        ContactNumber = c.String(),
                        UserCreated = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        UserModified = c.String(),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ClientId);
            
            CreateTable(
                "dbo.Collection",
                c => new
                    {
                        CollectionId = c.Int(nullable: false, identity: true),
                        CheckNumber = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderId = c.Int(nullable: false),
                        UserCreated = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        UserModified = c.String(),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CollectionId)
                .ForeignKey("dbo.Order", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        Terms = c.Int(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        Remarks = c.String(),
                        CustomerPONumber = c.String(),
                        ClientId = c.Int(nullable: false),
                        UserCreated = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        UserModified = c.String(),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Client", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        OrderDetailsId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        UserCreated = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        UserModified = c.String(),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OrderDetailsId)
                .ForeignKey("dbo.Order", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        BatchNumber = c.String(),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SellingPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MarkUp = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StockQuantity = c.Int(nullable: false),
                        ExpirationDate = c.DateTime(nullable: false),
                        UnitSizeId = c.Int(nullable: false),
                        Supplier = c.String(),
                        PurchaseDate = c.DateTime(nullable: false),
                        ProductGroupId = c.Int(nullable: false),
                        UserCreated = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        UserModified = c.String(),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.ProductGroup", t => t.ProductGroupId, cascadeDelete: true)
                .ForeignKey("dbo.UnitSize", t => t.UnitSizeId, cascadeDelete: true)
                .Index(t => t.UnitSizeId)
                .Index(t => t.ProductGroupId);
            
            CreateTable(
                "dbo.ProductGroup",
                c => new
                    {
                        ProductGroupId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        UserCreated = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        UserModified = c.String(),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ProductGroupId);
            
            CreateTable(
                "dbo.UnitSize",
                c => new
                    {
                        UnitSizeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        UserCreated = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        UserModified = c.String(),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UnitSizeId);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Collection", "OrderId", "dbo.Order");
            DropForeignKey("dbo.OrderDetails", "ProductId", "dbo.Product");
            DropForeignKey("dbo.Product", "UnitSizeId", "dbo.UnitSize");
            DropForeignKey("dbo.Product", "ProductGroupId", "dbo.ProductGroup");
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Order");
            DropForeignKey("dbo.Order", "ClientId", "dbo.Client");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Product", new[] { "ProductGroupId" });
            DropIndex("dbo.Product", new[] { "UnitSizeId" });
            DropIndex("dbo.OrderDetails", new[] { "ProductId" });
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropIndex("dbo.Order", new[] { "ClientId" });
            DropIndex("dbo.Collection", new[] { "OrderId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.UnitSize");
            DropTable("dbo.ProductGroup");
            DropTable("dbo.Product");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Order");
            DropTable("dbo.Collection");
            DropTable("dbo.Client");
        }
    }
}
