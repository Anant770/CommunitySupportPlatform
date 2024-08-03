namespace CommunitySupportPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Donations", "CompanyId", c => c.Int(nullable: false));
            AddColumn("dbo.Jobs", "ArticleId", c => c.Int(nullable: false));
            CreateIndex("dbo.Donations", "CompanyId");
            CreateIndex("dbo.Jobs", "ArticleId");
            AddForeignKey("dbo.Jobs", "ArticleId", "dbo.Articles", "ArticleId", cascadeDelete: true);
            AddForeignKey("dbo.Donations", "CompanyId", "dbo.Companies", "CompanyId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Donations", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Jobs", "ArticleId", "dbo.Articles");
            DropIndex("dbo.Jobs", new[] { "ArticleId" });
            DropIndex("dbo.Donations", new[] { "CompanyId" });
            DropColumn("dbo.Jobs", "ArticleId");
            DropColumn("dbo.Donations", "CompanyId");
        }
    }
}
