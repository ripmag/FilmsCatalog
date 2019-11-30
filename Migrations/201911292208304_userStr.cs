namespace FilmsCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userStr : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Films", "user", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Films", "user", c => c.Int(nullable: false));
        }
    }
}
