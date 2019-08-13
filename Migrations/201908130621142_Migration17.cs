namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration17 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "Id", c => c.Int(nullable: false, identity: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "Id", c => c.Int(nullable: false));
        }
    }
}
