namespace MvcDirectory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPersonalNotesToPerson : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "PersonalNotes", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("dbo.People", "PersonalNotes");
        }
    }
}
