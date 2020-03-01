namespace ComicBookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ComicDiscount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ComicModels", "isDiscounted", c => c.Boolean(nullable: false));
            AddColumn("dbo.ComicModels", "DiscountPrice", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ComicModels", "DiscountPrice");
            DropColumn("dbo.ComicModels", "isDiscounted");
        }
    }
}
