namespace Tools.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedExaminationsTypesAsTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Examination Natures",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        DeletedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Examination Reasons",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        DeletedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Examination Types",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        DeletedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Examinations", "ExaminationTypeFK", c => c.Long(nullable: false));
            AddColumn("dbo.Examinations", "ExaminationNatureFK", c => c.Long(nullable: false));
            AddColumn("dbo.Examinations", "ExaminationReasonFK", c => c.Long(nullable: false));
            CreateIndex("dbo.Examinations", "ExaminationTypeFK");
            CreateIndex("dbo.Examinations", "ExaminationNatureFK");
            CreateIndex("dbo.Examinations", "ExaminationReasonFK");
            AddForeignKey("dbo.Examinations", "ExaminationNatureFK", "dbo.Examination Natures", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Examinations", "ExaminationReasonFK", "dbo.Examination Reasons", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Examinations", "ExaminationTypeFK", "dbo.Examination Types", "Id", cascadeDelete: true);
            DropColumn("dbo.Examinations", "ExaminationType");
            DropColumn("dbo.Examinations", "ExaminationNature");
            DropColumn("dbo.Examinations", "ExaminationReason");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Examinations", "ExaminationReason", c => c.Int(nullable: false));
            AddColumn("dbo.Examinations", "ExaminationNature", c => c.Int(nullable: false));
            AddColumn("dbo.Examinations", "ExaminationType", c => c.Int(nullable: false));
            DropForeignKey("dbo.Examinations", "ExaminationTypeFK", "dbo.Examination Types");
            DropForeignKey("dbo.Examinations", "ExaminationReasonFK", "dbo.Examination Reasons");
            DropForeignKey("dbo.Examinations", "ExaminationNatureFK", "dbo.Examination Natures");
            DropIndex("dbo.Examinations", new[] { "ExaminationReasonFK" });
            DropIndex("dbo.Examinations", new[] { "ExaminationNatureFK" });
            DropIndex("dbo.Examinations", new[] { "ExaminationTypeFK" });
            DropColumn("dbo.Examinations", "ExaminationReasonFK");
            DropColumn("dbo.Examinations", "ExaminationNatureFK");
            DropColumn("dbo.Examinations", "ExaminationTypeFK");
            DropTable("dbo.Examination Types");
            DropTable("dbo.Examination Reasons");
            DropTable("dbo.Examination Natures");
        }
    }
}
