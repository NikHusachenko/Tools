using System.Data.Entity.ModelConfiguration;
using Tools.Database.Entities;

namespace Tools.EntityFramework.Configurations
{
    public class ExaminationConfiguration : EntityTypeConfiguration<ExaminutionEntity>
    {
        public ExaminationConfiguration()
        {
            ToTable("Examinations").HasKey(examination => examination.Id);

            HasRequired<ToolEntity>(examination => examination.Tool)
                .WithMany(tool => tool.Examinutions)
                .HasForeignKey(examination => examination.ToolFK)
                .WillCascadeOnDelete(true);
        }
    }
}