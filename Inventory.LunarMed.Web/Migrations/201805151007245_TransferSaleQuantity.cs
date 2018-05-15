namespace Inventory.LunarMed.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TransferSaleQuantity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SaleDetails", "Quantity", c => c.Int(nullable: false));
            DropColumn("dbo.Sale", "Quantity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sale", "Quantity", c => c.Int(nullable: false));
            DropColumn("dbo.SaleDetails", "Quantity");
        }
    }
}
