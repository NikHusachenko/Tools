using System.Data.Entity.ModelConfiguration;
using Tools.Database.Entities;

namespace Tools.EntityFramework.Configurations
{
    public class ExaminationReasonConfiguration : EntityTypeConfiguration<ExaminationReasonEntity>
    {
        public ExaminationReasonConfiguration()
        {
            ToTable("Examination Reasons").HasKey(reason => reason.Id);
        }
    }
}