namespace ComicBookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userComicUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ComicModels", "UserComicModel_UserId", "dbo.UserComicModels");
            DropForeignKey("dbo.ComicModels", "UserComicModel_UserId1", "dbo.UserComicModels");
            DropIndex("dbo.ComicModels", new[] { "UserComicModel_UserId" });
            DropIndex("dbo.ComicModels", new[] { "UserComicModel_UserId1" });
            DropColumn("dbo.ComicModels", "UserComicModel_UserId");
            DropColumn("dbo.ComicModels", "UserComicModel_UserId1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ComicModels", "UserComicModel_UserId1", c => c.String(maxLength: 128));
            AddColumn("dbo.ComicModels", "UserComicModel_UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.ComicModels", "UserComicModel_UserId1");
            CreateIndex("dbo.ComicModels", "UserComicModel_UserId");
            AddForeignKey("dbo.ComicModels", "UserComicModel_UserId1", "dbo.UserComicModels", "UserId");
            AddForeignKey("dbo.ComicModels", "UserComicModel_UserId", "dbo.UserComicModels", "UserId");
        }
    }
}
