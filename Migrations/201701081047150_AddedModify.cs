namespace HeroAcademy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedModify : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HeroLessons", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HeroLessons", "IsDeleted");
        }
    }
}
