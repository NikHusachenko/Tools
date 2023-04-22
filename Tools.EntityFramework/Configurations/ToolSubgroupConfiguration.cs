using System.Data.Entity.ModelConfiguration;
using Tools.Database.Entities;

namespace Tools.EntityFramework.Configurations
{
    public class ToolSubgroupConfiguration : EntityTypeConfiguration<ToolSubgroupEntity>
    {
        public ToolSubgroupConfiguration()
        {
            ToTable("Tool Subgroups").HasKey(subgroup => subgroup.Id);
        }
    }
}