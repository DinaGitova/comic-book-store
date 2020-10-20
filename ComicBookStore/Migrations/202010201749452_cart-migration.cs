namespace ComicBookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cartmigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserComicModelComicModels",
                c => new
                    {
                        UserComicModel_UserId = c.String(nullable: false, maxLength: 128),
                        ComicModel_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserComicModel_UserId, t.ComicModel_Id })
                .ForeignKey("dbo.UserComicModels", t => t.UserComicModel_UserId, cascadeDelete: true)
                .ForeignKey("dbo.ComicModels", t => t.ComicModel_Id, cascadeDelete: true)
                .Index(t => t.UserComicModel_UserId)
                .Index(t => t.ComicModel_Id);
            
            CreateTable(
                "dbo.UserComicModelComicModel1",
                c => new
                    {
                        UserComicModel_UserId = c.String(nullable: false, maxLength: 128),
                        ComicModel_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserComicModel_UserId, t.ComicModel_Id })
                .ForeignKey("dbo.UserComicModels", t => t.UserComicModel_UserId, cascadeDelete: true)
                .ForeignKey("dbo.ComicModels", t => t.ComicModel_Id, cascadeDelete: true)
                .Index(t => t.UserComicModel_UserId)
                .Index(t => t.ComicModel_Id);
            
            DropColumn("dbo.UserComicModels", "FavouriteComics");
            DropColumn("dbo.UserComicModels", "Cart");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserComicModels", "Cart", c => c.String());
            AddColumn("dbo.UserComicModels", "FavouriteComics", c => c.String());
            DropForeignKey("dbo.UserComicModelComicModel1", "ComicModel_Id", "dbo.ComicModels");
            DropForeignKey("dbo.UserComicModelComicModel1", "UserComicModel_UserId", "dbo.UserComicModels");
            DropForeignKey("dbo.UserComicModelComicModels", "ComicModel_Id", "dbo.ComicModels");
            DropForeignKey("dbo.UserComicModelComicModels", "UserComicModel_UserId", "dbo.UserComicModels");
            DropIndex("dbo.UserComicModelComicModel1", new[] { "ComicModel_Id" });
            DropIndex("dbo.UserComicModelComicModel1", new[] { "UserComicModel_UserId" });
            DropIndex("dbo.UserComicModelComicModels", new[] { "ComicModel_Id" });
            DropIndex("dbo.UserComicModelComicModels", new[] { "UserComicModel_UserId" });
            DropTable("dbo.UserComicModelComicModel1");
            DropTable("dbo.UserComicModelComicModels");
        }
    }
}
