namespace Inventory.LunarMed.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTotalInOrderTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "Total", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "Total");
        }
    }
}
