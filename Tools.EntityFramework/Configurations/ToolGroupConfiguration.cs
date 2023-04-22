using System.Data.Entity.ModelConfiguration;
using Tools.Database.Entities;

namespace Tools.EntityFramework.Configurations
{
    public class ToolGroupConfiguration : EntityTypeConfiguration<ToolGroupEntity>
    {
        public ToolGroupConfiguration()
        {
            ToTable("Tool Groups").HasKey(group => group.Id);

            HasMany<ToolSubgroupEntity>(group => group.Subgroups)
                .WithRequired(subgroup => subgroup.Group)
                .HasForeignKey(group => group.GroupFK);
        }
    }
}