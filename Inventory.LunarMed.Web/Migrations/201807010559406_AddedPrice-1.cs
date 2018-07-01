namespace Inventory.LunarMed.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPrice1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Price",
                c => new
                    {
                        PriceId = c.Int(nullable: false, identity: true),
                        BrandId = c.Int(nullable: false),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UnitSizeId = c.Int(nullable: false),
                        ClientId = c.Int(nullable: false),
                        UserCreated = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        UserModified = c.String(),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PriceId)
                .ForeignKey("dbo.Brand", t => t.BrandId, cascadeDelete: true)
                .ForeignKey("dbo.Client", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.UnitSize", t => t.UnitSizeId, cascadeDelete: true)
                .Index(t => t.BrandId)
                .Index(t => t.UnitSizeId)
                .Index(t => t.ClientId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Price", "UnitSizeId", "dbo.UnitSize");
            DropForeignKey("dbo.Price", "ClientId", "dbo.Client");
            DropForeignKey("dbo.Price", "BrandId", "dbo.Brand");
            DropIndex("dbo.Price", new[] { "ClientId" });
            DropIndex("dbo.Price", new[] { "UnitSizeId" });
            DropIndex("dbo.Price", new[] { "BrandId" });
            DropTable("dbo.Price");
        }
    }
}
