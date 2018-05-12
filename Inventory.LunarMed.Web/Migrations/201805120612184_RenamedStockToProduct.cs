namespace Inventory.LunarMed.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenamedStockToProduct : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Stock", "UnitSizeId", "dbo.UnitSize");
            DropForeignKey("dbo.Sale", "StockId", "dbo.Stock");
            DropForeignKey("dbo.Sale", "StockGroupId", "dbo.StockGroup");
            DropIndex("dbo.Sale", new[] { "StockId" });
            DropIndex("dbo.Sale", new[] { "StockGroupId" });
            DropIndex("dbo.Stock", new[] { "UnitSizeId" });
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ProductkId = c.Int(nullable: false, identity: true),
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
                        UserCreated = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        UserModified = c.String(),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ProductkId)
                .ForeignKey("dbo.UnitSize", t => t.UnitSizeId, cascadeDelete: true)
                .Index(t => t.UnitSizeId);
            
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
            
            AddColumn("dbo.Sale", "ProductId", c => c.Int(nullable: false));
            AddColumn("dbo.Sale", "ProductGroupId", c => c.Int(nullable: false));
            CreateIndex("dbo.Sale", "ProductId");
            CreateIndex("dbo.Sale", "ProductGroupId");
            AddForeignKey("dbo.Sale", "ProductId", "dbo.Product", "ProductkId", cascadeDelete: true);
            AddForeignKey("dbo.Sale", "ProductGroupId", "dbo.ProductGroup", "ProductGroupId", cascadeDelete: true);
            DropColumn("dbo.Sale", "StockId");
            DropColumn("dbo.Sale", "StockGroupId");
            DropTable("dbo.Stock");
            DropTable("dbo.StockGroup");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.StockId);
            
            AddColumn("dbo.Sale", "StockGroupId", c => c.Int(nullable: false));
            AddColumn("dbo.Sale", "StockId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Sale", "ProductGroupId", "dbo.ProductGroup");
            DropForeignKey("dbo.Sale", "ProductId", "dbo.Product");
            DropForeignKey("dbo.Product", "UnitSizeId", "dbo.UnitSize");
            DropIndex("dbo.Product", new[] { "UnitSizeId" });
            DropIndex("dbo.Sale", new[] { "ProductGroupId" });
            DropIndex("dbo.Sale", new[] { "ProductId" });
            DropColumn("dbo.Sale", "ProductGroupId");
            DropColumn("dbo.Sale", "ProductId");
            DropTable("dbo.ProductGroup");
            DropTable("dbo.Product");
            CreateIndex("dbo.Stock", "UnitSizeId");
            CreateIndex("dbo.Sale", "StockGroupId");
            CreateIndex("dbo.Sale", "StockId");
            AddForeignKey("dbo.Sale", "StockGroupId", "dbo.StockGroup", "StockGroupId", cascadeDelete: true);
            AddForeignKey("dbo.Sale", "StockId", "dbo.Stock", "StockId", cascadeDelete: true);
            AddForeignKey("dbo.Stock", "UnitSizeId", "dbo.UnitSize", "UnitSizeId", cascadeDelete: true);
        }
    }
}
