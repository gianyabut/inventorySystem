namespace Inventory.LunarMed.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GenericBrandTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brand",
                c => new
                    {
                        BrandId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        GenericId = c.Int(nullable: false),
                        UserCreated = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        UserModified = c.String(),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.BrandId)
                .ForeignKey("dbo.Generic", t => t.GenericId, cascadeDelete: true)
                .Index(t => t.GenericId);
            
            CreateTable(
                "dbo.Generic",
                c => new
                    {
                        GenericId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        UserCreated = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        UserModified = c.String(),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.GenericId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Brand", "GenericId", "dbo.Generic");
            DropIndex("dbo.Brand", new[] { "GenericId" });
            DropTable("dbo.Generic");
            DropTable("dbo.Brand");
        }
    }
}
