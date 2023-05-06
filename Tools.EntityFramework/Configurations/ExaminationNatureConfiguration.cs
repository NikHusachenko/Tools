using System.Data.Entity.ModelConfiguration;
using Tools.Database.Entities;

namespace Tools.EntityFramework.Configurations
{
    public class ExaminationNatureConfiguration : EntityTypeConfiguration<ExaminationNatureEntity>
    {
        public ExaminationNatureConfiguration()
        {
            ToTable("Examination Natures").HasKey(nature => nature.Id);
        }
    }
}