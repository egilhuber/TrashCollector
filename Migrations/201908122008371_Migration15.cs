namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration15 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "PickupDate", c => c.String());
            DropTable("dbo.PendingPickups");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PendingPickups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicationId = c.String(),
                        PickupDate = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Customers", "PickupDate");
        }
    }
}
