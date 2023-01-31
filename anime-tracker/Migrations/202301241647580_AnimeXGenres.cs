namespace anime_tracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnimeXGenres : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnimeXGenres",
                c => new
                    {
                        animexgenre_id = c.Int(nullable: false, identity: true),
                        anime_id = c.Int(nullable: false),
                        genre_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.animexgenre_id)
                .ForeignKey("dbo.Animes", t => t.anime_id, cascadeDelete: true)
                .ForeignKey("dbo.Genres", t => t.genre_id, cascadeDelete: true)
                .Index(t => t.anime_id)
                .Index(t => t.genre_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AnimeXGenres", "genre_id", "dbo.Genres");
            DropForeignKey("dbo.AnimeXGenres", "anime_id", "dbo.Animes");
            DropIndex("dbo.AnimeXGenres", new[] { "genre_id" });
            DropIndex("dbo.AnimeXGenres", new[] { "anime_id" });
            DropTable("dbo.AnimeXGenres");
        }
    }
}
