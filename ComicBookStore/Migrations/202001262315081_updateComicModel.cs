namespace ComicBookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateComicModel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ComicModels", "isFavourite");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ComicModels", "isFavourite", c => c.Boolean(nullable: false));
        }
    }
}
