namespace Inventory.LunarMed.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSaleDetails : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sale", "ProductId", "dbo.Product");
            DropForeignKey("dbo.Sale", "ProductGroupId", "dbo.ProductGroup");
            DropIndex("dbo.Sale", new[] { "ProductId" });
            DropIndex("dbo.Sale", new[] { "ProductGroupId" });
            CreateTable(
                "dbo.SaleDetails",
                c => new
                    {
                        SaleDetailsId = c.Int(nullable: false, identity: true),
                        SaleId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        ProductGroupId = c.Int(nullable: false),
                        UserCreated = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        UserModified = c.String(),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SaleDetailsId)
                .ForeignKey("dbo.Sale", t => t.SaleId, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.ProductGroup", t => t.ProductGroupId, cascadeDelete: true)
                .Index(t => t.SaleId)
                .Index(t => t.ProductId)
                .Index(t => t.ProductGroupId);
            
            DropColumn("dbo.Sale", "ProductId");
            DropColumn("dbo.Sale", "ProductGroupId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sale", "ProductGroupId", c => c.Int(nullable: false));
            AddColumn("dbo.Sale", "ProductId", c => c.Int(nullable: false));
            DropForeignKey("dbo.SaleDetails", "ProductGroupId", "dbo.ProductGroup");
            DropForeignKey("dbo.SaleDetails", "ProductId", "dbo.Product");
            DropForeignKey("dbo.SaleDetails", "SaleId", "dbo.Sale");
            DropIndex("dbo.SaleDetails", new[] { "ProductGroupId" });
            DropIndex("dbo.SaleDetails", new[] { "ProductId" });
            DropIndex("dbo.SaleDetails", new[] { "SaleId" });
            DropTable("dbo.SaleDetails");
            CreateIndex("dbo.Sale", "ProductGroupId");
            CreateIndex("dbo.Sale", "ProductId");
            AddForeignKey("dbo.Sale", "ProductGroupId", "dbo.ProductGroup", "ProductGroupId", cascadeDelete: true);
            AddForeignKey("dbo.Sale", "ProductId", "dbo.Product", "ProductId", cascadeDelete: true);
        }
    }
}
