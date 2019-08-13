namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NinthMigration : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Employees", new[] { "ApplicationId" });
            DropPrimaryKey("dbo.Employees");
            AlterColumn("dbo.Employees", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Employees", "ApplicationId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Employees", "ApplicationId");
            CreateIndex("dbo.Employees", "ApplicationId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Employees", new[] { "ApplicationId" });
            DropPrimaryKey("dbo.Employees");
            AlterColumn("dbo.Employees", "ApplicationId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Employees", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Employees", "Id");
            CreateIndex("dbo.Employees", "ApplicationId");
        }
    }
}
