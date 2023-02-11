namespace anime_tracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAnimes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Animes", "start_date", c => c.DateTime(nullable: false));
            DropColumn("dbo.Animes", "air_date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Animes", "air_date", c => c.DateTime(nullable: false));
            DropColumn("dbo.Animes", "start_date");
        }
    }
}
