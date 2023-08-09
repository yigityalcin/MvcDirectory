namespace MvcDirectory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialMigration31 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "Photo1", c => c.String());
            DropColumn("dbo.People", "Photo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.People", "Photo", c => c.Binary());
            DropColumn("dbo.People", "Photo1");
        }
    }
}
