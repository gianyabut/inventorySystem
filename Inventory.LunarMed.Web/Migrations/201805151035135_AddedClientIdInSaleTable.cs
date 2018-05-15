namespace Inventory.LunarMed.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedClientIdInSaleTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sale", "ClientId", c => c.Int(nullable: false));
            CreateIndex("dbo.Sale", "ClientId");
            AddForeignKey("dbo.Sale", "ClientId", "dbo.Client", "ClientId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sale", "ClientId", "dbo.Client");
            DropIndex("dbo.Sale", new[] { "ClientId" });
            DropColumn("dbo.Sale", "ClientId");
        }
    }
}
