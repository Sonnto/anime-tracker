namespace anime_tracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Animes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Animes",
                c => new
                    {
                        anime_id = c.Int(nullable: false, identity: true),
                        anime_title = c.String(),
                        anime_type_id = c.Int(nullable: false),
                        rating = c.Int(nullable: false),
                        completed_episodes = c.Int(nullable: false),
                        total_episodes = c.Int(nullable: false),
                        status = c.String(),
                        air_date = c.DateTime(nullable: false),
                        end_date = c.DateTime(nullable: false),
                        activity = c.String(),
                        favorite = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.anime_id)
                .ForeignKey("dbo.AnimeTypes", t => t.anime_type_id, cascadeDelete: true)
                .Index(t => t.anime_type_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Animes", "anime_type_id", "dbo.AnimeTypes");
            DropIndex("dbo.Animes", new[] { "anime_type_id" });
            DropTable("dbo.Animes");
        }
    }
}
