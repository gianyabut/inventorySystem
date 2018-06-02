namespace Inventory.LunarMed.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedGovTax : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Collection", "IsGovTax", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Collection", "IsGovTax");
        }
    }
}
