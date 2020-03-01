namespace ComicBookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateComi : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserComicModels", "FavouriteComics", c => c.String());
            AddColumn("dbo.UserComicModels", "Cart", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserComicModels", "Cart");
            DropColumn("dbo.UserComicModels", "FavouriteComics");
        }
    }
}
