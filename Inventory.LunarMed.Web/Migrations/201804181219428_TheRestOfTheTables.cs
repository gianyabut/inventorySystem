namespace Inventory.LunarMed.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TheRestOfTheTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Collection",
                c => new
                    {
                        CollectionId = c.Int(nullable: false, identity: true),
                        CheckNumber = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SalesId = c.Int(nullable: false),
                        UserCreated = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        UserModified = c.String(),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CollectionId)
                .ForeignKey("dbo.Sale", t => t.SalesId, cascadeDelete: true)
                .Index(t => t.SalesId);
            
            CreateTable(
                "dbo.Sale",
                c => new
                    {
                        SalesId = c.Int(nullable: false, identity: true),
                        StockId = c.Int(nullable: false),
                        StockGroupId = c.Int(nullable: false),
                        Terms = c.Int(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        Remarks = c.String(),
                        Quantity = c.Int(nullable: false),
                        CustomerPONumber = c.String(),
                        UserCreated = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        UserModified = c.String(),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SalesId)
                .ForeignKey("dbo.Stock", t => t.StockId, cascadeDelete: true)
                .ForeignKey("dbo.StockGroup", t => t.StockGroupId, cascadeDelete: true)
                .Index(t => t.StockId)
                .Index(t => t.StockGroupId);
            
            CreateTable(
                "dbo.Stock",
                c => new
                    {
                        StockId = c.Int(nullable: false, identity: true),
                        StockName = c.String(),
                        BatchNumber = c.String(),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SellingPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MarkUp = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StockQuantity = c.Int(nullable: false),
                        ExpirationDate = c.DateTime(nullable: false),
                        UnitSizeId = c.Int(nullable: false),
                        Supplier = c.String(),
                        PurchaseDate = c.DateTime(nullable: false),
                        UserCreated = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        UserModified = c.String(),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.StockId)
                .ForeignKey("dbo.UnitSize", t => t.UnitSizeId, cascadeDelete: true)
                .Index(t => t.UnitSizeId);
            
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
                "dbo.StockGroup",
                c => new
                    {
                        StockGroupId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        UserCreated = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        UserModified = c.String(),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.StockGroupId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Collection", "SalesId", "dbo.Sale");
            DropForeignKey("dbo.Sale", "StockGroupId", "dbo.StockGroup");
            DropForeignKey("dbo.Sale", "StockId", "dbo.Stock");
            DropForeignKey("dbo.Stock", "UnitSizeId", "dbo.UnitSize");
            DropIndex("dbo.Stock", new[] { "UnitSizeId" });
            DropIndex("dbo.Sale", new[] { "StockGroupId" });
            DropIndex("dbo.Sale", new[] { "StockId" });
            DropIndex("dbo.Collection", new[] { "SalesId" });
            DropTable("dbo.StockGroup");
            DropTable("dbo.UnitSize");
            DropTable("dbo.Stock");
            DropTable("dbo.Sale");
            DropTable("dbo.Collection");
        }
    }
}
