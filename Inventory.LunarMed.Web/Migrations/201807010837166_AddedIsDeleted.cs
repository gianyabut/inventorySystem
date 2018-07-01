namespace Inventory.LunarMed.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIsDeleted : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "IsDeleted");
        }
    }
}
