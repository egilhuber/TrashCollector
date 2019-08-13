namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration16 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "Id", c => c.Int(nullable: false, identity: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "Id", c => c.Int(nullable: false));
        }
    }
}
