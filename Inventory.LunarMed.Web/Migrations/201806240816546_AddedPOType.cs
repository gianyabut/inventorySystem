namespace Inventory.LunarMed.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPOType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "Type", c => c.String(maxLength: 1, fixedLength: true, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "Type");
        }
    }
}
