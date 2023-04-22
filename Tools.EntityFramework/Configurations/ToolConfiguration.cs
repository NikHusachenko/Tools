using System.Data.Entity.ModelConfiguration;
using Tools.Database.Entities;

namespace Tools.EntityFramework.Configurations
{
    public class ToolConfiguration : EntityTypeConfiguration<ToolEntity>
    {
        public ToolConfiguration()
        {
            ToTable("Tools").HasKey(tool => tool.Id);

            HasRequired<ToolSubgroupEntity>(tool => tool.Subgroup)
                .WithMany(subgroup => subgroup.Tools)
                .HasForeignKey(tool => tool.SubgroupFK);
        }
    }
}