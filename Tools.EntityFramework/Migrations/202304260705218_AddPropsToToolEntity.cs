namespace Tools.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPropsToToolEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tools", "Name", c => c.String());
            AddColumn("dbo.Tools", "Brand", c => c.String());
            AddColumn("dbo.Tools", "Registration", c => c.Int(nullable: false));
            AddColumn("dbo.Tools", "RegistrationNumber", c => c.String());
            AddColumn("dbo.Tools", "IntraFactoryNumber", c => c.String());
            AddColumn("dbo.Tools", "FactoryNumber", c => c.String());
            AddColumn("dbo.Tools", "CreatingDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Tools", "CommissioningDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Tools", "ExpirationYear", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tools", "ExpirationYear");
            DropColumn("dbo.Tools", "CommissioningDate");
            DropColumn("dbo.Tools", "CreatingDate");
            DropColumn("dbo.Tools", "FactoryNumber");
            DropColumn("dbo.Tools", "IntraFactoryNumber");
            DropColumn("dbo.Tools", "RegistrationNumber");
            DropColumn("dbo.Tools", "Registration");
            DropColumn("dbo.Tools", "Brand");
            DropColumn("dbo.Tools", "Name");
        }
    }
}
