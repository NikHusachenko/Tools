namespace Tools.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActualExaminationDateCanBeNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Examinations", "ActualExaminationDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Examinations", "ActualExaminationDate", c => c.DateTime(nullable: false));
        }
    }
}
