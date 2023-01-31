namespace anime_tracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Genres : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        genre_id = c.Int(nullable: false, identity: true),
                        genre_name = c.String(),
                    })
                .PrimaryKey(t => t.genre_id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Genres");
        }
    }
}
