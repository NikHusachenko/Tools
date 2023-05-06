using System.Data.Entity.ModelConfiguration;
using Tools.Database.Entities;

namespace Tools.EntityFramework.Configurations
{
    public class ExaminationConfiguration : EntityTypeConfiguration<ExaminationEntity>
    {
        public ExaminationConfiguration()
        {
            ToTable("Examinations").HasKey(examination => examination.Id);

            HasRequired<ToolEntity>(examination => examination.Tool)
                .WithMany(tool => tool.Examinutions)
                .HasForeignKey(examination => examination.ToolFK)
                .WillCascadeOnDelete(true);

            HasRequired<ExaminationNatureEntity>(examination => examination.ExaminationNature)
                .WithMany(nature => nature.Examinations)
                .HasForeignKey(examination => examination.ExaminationNatureFK);

            HasRequired<ExaminationReasonEntity>(examination => examination.ExaminationReason)
                .WithMany(reason => reason.Examination)
                .HasForeignKey(examination => examination.ExaminationReasonFK);

            HasRequired<ExaminationTypeEntity>(examination => examination.ExaminationType)
                .WithMany(type => type.Examinutions)
                .HasForeignKey(examination => examination.ExaminationTypeFK);
        }
    }
}