namespace HeroAcademy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DOit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ProfilePicture", c => c.Binary());
            DropColumn("dbo.AspNetUsers", "UserPhoto");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "UserPhoto", c => c.Binary());
            DropColumn("dbo.AspNetUsers", "ProfilePicture");
        }
    }
}
