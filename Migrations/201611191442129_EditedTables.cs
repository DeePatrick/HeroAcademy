namespace HeroAcademy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditedTables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HeroLessons", "FullName", c => c.String(nullable: false));
            DropColumn("dbo.HeroLessons", "Name");
            DropColumn("dbo.HeroLessons", "IsDeleted");
            DropColumn("dbo.HeroLessons", "StudentGrade");
        }
        
        public override void Down()
        {
            AddColumn("dbo.HeroLessons", "StudentGrade", c => c.String());
            AddColumn("dbo.HeroLessons", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.HeroLessons", "Name", c => c.String());
            DropColumn("dbo.HeroLessons", "FullName");
        }
    }
}
