namespace ComicBookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatecomic : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ComicModels", "Writer", c => c.String());
            AddColumn("dbo.ComicModels", "inCarousel", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ComicModels", "inCarousel");
            DropColumn("dbo.ComicModels", "Writer");
        }
    }
}
