namespace Inventory.LunarMed.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSRPDC : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stock", "SRPDC", c => c.Decimal(nullable: false, defaultValueSql: "0.00", precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Stock", "SRPDC");
        }
    }
}
