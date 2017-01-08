namespace HeroAcademy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AccountPhoto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "UserPhoto", c => c.Binary());
            AlterColumn("dbo.HeroLessons", "Photo", c => c.String(nullable: false));
            AlterColumn("dbo.HeroLessons", "AlternateText", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.HeroLessons", "AlternateText", c => c.String());
            AlterColumn("dbo.HeroLessons", "Photo", c => c.String());
            DropColumn("dbo.AspNetUsers", "UserPhoto");
        }
    }
}
