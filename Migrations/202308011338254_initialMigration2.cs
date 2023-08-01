namespace MvcDirectory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialMigration2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "Photo", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.People", "Photo");
        }
    }
}
