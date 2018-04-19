namespace Inventory.LunarMed.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompletedTables : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Client");
            DropColumn("dbo.Client", "ClientId");
            AddColumn("dbo.Client", "ClientId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Client", "ClientId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Client");
            DropColumn("dbo.Client", "ClientId");
            AddColumn("dbo.Client", "ClientId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Client", "ClientId");
        }
    }
}
