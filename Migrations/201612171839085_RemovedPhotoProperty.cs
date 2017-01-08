namespace HeroAcademy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedPhotoProperty : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.HeroLessons", "Photo");
            DropColumn("dbo.HeroLessons", "AlternateText");
        }
        
        public override void Down()
        {
            AddColumn("dbo.HeroLessons", "AlternateText", c => c.String(nullable: false));
            AddColumn("dbo.HeroLessons", "Photo", c => c.String(nullable: false));
        }
    }
}
