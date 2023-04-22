namespace Tools.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tool Groups",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tool Subgroups",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        GroupFK = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tool Groups", t => t.GroupFK, cascadeDelete: true)
                .Index(t => t.GroupFK);
            
            CreateTable(
                "dbo.Tools",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        OrganizationalType = c.Int(nullable: false),
                        SubgroupFK = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tool Subgroups", t => t.SubgroupFK, cascadeDelete: true)
                .Index(t => t.SubgroupFK);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tool Subgroups", "GroupFK", "dbo.Tool Groups");
            DropForeignKey("dbo.Tools", "SubgroupFK", "dbo.Tool Subgroups");
            DropIndex("dbo.Tools", new[] { "SubgroupFK" });
            DropIndex("dbo.Tool Subgroups", new[] { "GroupFK" });
            DropTable("dbo.Tools");
            DropTable("dbo.Tool Subgroups");
            DropTable("dbo.Tool Groups");
        }
    }
}
