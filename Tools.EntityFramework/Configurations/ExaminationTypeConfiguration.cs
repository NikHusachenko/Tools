using System.Data.Entity.ModelConfiguration;
using Tools.Database.Entities;

namespace Tools.EntityFramework.Configurations
{
    public class ExaminationTypeConfiguration : EntityTypeConfiguration<ExaminationTypeEntity>
    {
        public ExaminationTypeConfiguration()
        {
            ToTable("Examination Types").HasKey(type => type.Id);
        }
    }
}