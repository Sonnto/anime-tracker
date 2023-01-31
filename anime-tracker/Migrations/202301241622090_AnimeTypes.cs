namespace anime_tracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnimeTypes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnimeTypes",
                c => new
                    {
                        anime_type_id = c.Int(nullable: false, identity: true),
                        anime_type_name = c.String(),
                    })
                .PrimaryKey(t => t.anime_type_id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AnimeTypes");
        }
    }
}
