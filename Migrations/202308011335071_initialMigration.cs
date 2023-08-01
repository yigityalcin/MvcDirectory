namespace MvcDirectory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialMigration : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.People", "Picture");
        }
        
        public override void Down()
        {
            AddColumn("dbo.People", "Picture", c => c.Binary());
        }
    }
}
