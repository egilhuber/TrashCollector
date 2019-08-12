namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EighthMigration : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Customers", new[] { "ApplicationId" });
            DropPrimaryKey("dbo.Customers");
            AlterColumn("dbo.Customers", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Customers", "ApplicationId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Customers", "ApplicationId");
            CreateIndex("dbo.Customers", "ApplicationId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Customers", new[] { "ApplicationId" });
            DropPrimaryKey("dbo.Customers");
            AlterColumn("dbo.Customers", "ApplicationId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Customers", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Customers", "Id");
            CreateIndex("dbo.Customers", "ApplicationId");
        }
    }
}
