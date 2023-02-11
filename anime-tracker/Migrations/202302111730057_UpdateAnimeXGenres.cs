namespace anime_tracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAnimeXGenres : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GenreAnimes",
                c => new
                    {
                        Genre_genre_id = c.Int(nullable: false),
                        Anime_anime_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Genre_genre_id, t.Anime_anime_id })
                .ForeignKey("dbo.Genres", t => t.Genre_genre_id, cascadeDelete: true)
                .ForeignKey("dbo.Animes", t => t.Anime_anime_id, cascadeDelete: true)
                .Index(t => t.Genre_genre_id)
                .Index(t => t.Anime_anime_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GenreAnimes", "Anime_anime_id", "dbo.Animes");
            DropForeignKey("dbo.GenreAnimes", "Genre_genre_id", "dbo.Genres");
            DropIndex("dbo.GenreAnimes", new[] { "Anime_anime_id" });
            DropIndex("dbo.GenreAnimes", new[] { "Genre_genre_id" });
            DropTable("dbo.GenreAnimes");
        }
    }
}
