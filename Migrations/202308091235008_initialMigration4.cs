namespace MvcDirectory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialMigration4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "CroppedPhoto", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.People", "CroppedPhoto");
        }
    }
}
