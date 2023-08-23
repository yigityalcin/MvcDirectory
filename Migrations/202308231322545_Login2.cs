namespace MvcDirectory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Login2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Kayits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        username = c.String(),
                        password = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Kayits");
        }
    }
}
