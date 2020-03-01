namespace ComicBookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userComic11 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserComicModels",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId);
            
            AddColumn("dbo.ComicModels", "UserComicModel_UserId", c => c.String(maxLength: 128));
            AddColumn("dbo.ComicModels", "UserComicModel_UserId1", c => c.String(maxLength: 128));
            CreateIndex("dbo.ComicModels", "UserComicModel_UserId");
            CreateIndex("dbo.ComicModels", "UserComicModel_UserId1");
            AddForeignKey("dbo.ComicModels", "UserComicModel_UserId", "dbo.UserComicModels", "UserId");
            AddForeignKey("dbo.ComicModels", "UserComicModel_UserId1", "dbo.UserComicModels", "UserId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ComicModels", "UserComicModel_UserId1", "dbo.UserComicModels");
            DropForeignKey("dbo.ComicModels", "UserComicModel_UserId", "dbo.UserComicModels");
            DropIndex("dbo.ComicModels", new[] { "UserComicModel_UserId1" });
            DropIndex("dbo.ComicModels", new[] { "UserComicModel_UserId" });
            DropColumn("dbo.ComicModels", "UserComicModel_UserId1");
            DropColumn("dbo.ComicModels", "UserComicModel_UserId");
            DropTable("dbo.UserComicModels");
        }
    }
}
