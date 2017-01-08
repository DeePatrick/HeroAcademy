namespace HeroAcademy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.HeroLessons", "Photo", c => c.String());
            AlterColumn("dbo.HeroLessons", "AlternateText", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.HeroLessons", "AlternateText", c => c.String(nullable: false));
            AlterColumn("dbo.HeroLessons", "Photo", c => c.String(nullable: false));
        }
    }
}
