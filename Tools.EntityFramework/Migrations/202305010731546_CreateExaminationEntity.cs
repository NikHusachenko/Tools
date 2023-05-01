namespace Tools.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateExaminationEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Examinations",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ExaminationType = c.Int(nullable: false),
                        ExaminationNature = c.Int(nullable: false),
                        ExaminationReason = c.Int(nullable: false),
                        ScheduleExaminationDate = c.DateTime(nullable: false),
                        ActualExaminationDate = c.DateTime(nullable: false),
                        ExaminationResult = c.String(),
                        ToolFK = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tools", t => t.ToolFK, cascadeDelete: true)
                .Index(t => t.ToolFK);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Examinations", "ToolFK", "dbo.Tools");
            DropIndex("dbo.Examinations", new[] { "ToolFK" });
            DropTable("dbo.Examinations");
        }
    }
}
