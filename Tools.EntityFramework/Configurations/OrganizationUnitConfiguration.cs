using System.Data.Entity.ModelConfiguration;
using Tools.Database.Entities;

namespace Tools.EntityFramework.Configurations
{
    public class OrganizationUnitConfiguration : EntityTypeConfiguration<OrganizationUnitEntity>
    {
        public OrganizationUnitConfiguration()
        {
            ToTable("Organization units").HasKey(unit => unit.Id);
        }
    }
}