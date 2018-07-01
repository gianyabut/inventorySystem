namespace Inventory.LunarMed.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTransactionNumberInCollections : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Collection", "TransactionNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Collection", "TransactionNumber");
        }
    }
}
