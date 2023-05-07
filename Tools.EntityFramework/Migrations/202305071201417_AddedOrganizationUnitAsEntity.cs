namespace Tools.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedOrganizationUnitAsEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Organization units",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        DeletedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Tools", "OrganizationUnitFK", c => c.Long(nullable: false));
            CreateIndex("dbo.Tools", "OrganizationUnitFK");
            AddForeignKey("dbo.Tools", "OrganizationUnitFK", "dbo.Organization units", "Id", cascadeDelete: true);
            DropColumn("dbo.Tools", "OrganizationalType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tools", "OrganizationalType", c => c.Int(nullable: false));
            DropForeignKey("dbo.Tools", "OrganizationUnitFK", "dbo.Organization units");
            DropIndex("dbo.Tools", new[] { "OrganizationUnitFK" });
            DropColumn("dbo.Tools", "OrganizationUnitFK");
            DropTable("dbo.Organization units");
        }
    }
}
