namespace Inventory.LunarMed.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSalesInvoice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "SalesInvoice", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "SalesInvoice");
        }
    }
}
