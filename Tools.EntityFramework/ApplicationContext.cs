using System.Data.Entity;
using Tools.Database.Entities;
using Tools.EntityFramework.Configurations;

namespace Tools.EntityFramework
{
	public class ApplicationDbContext : DbContext
	{
		public DbSet<ToolEntity> Tools { get; set; }
		public DbSet<ToolGroupEntity> Groups { get; set; }
		public DbSet<ToolSubgroupEntity> Subgroups { get; set; }
        public DbSet<ExaminationEntity> Examinutions { get; set; }
        public DbSet<ExaminationNatureEntity> Natures { get; set; }
        public DbSet<ExaminationReasonEntity> Reasons { get; set; }
        public DbSet<ExaminationTypeEntity> Types { get; set; }

        public ApplicationDbContext() : base(Common.Configuration.DEFAULT_CONNECTION)
		{
            Database.CreateIfNotExists();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ToolConfiguration());
            modelBuilder.Configurations.Add(new ToolGroupConfiguration());
            modelBuilder.Configurations.Add(new ToolSubgroupConfiguration());
            modelBuilder.Configurations.Add(new ExaminationConfiguration());
            modelBuilder.Configurations.Add(new ExaminationNatureConfiguration());
            modelBuilder.Configurations.Add(new ExaminationReasonConfiguration());
            modelBuilder.Configurations.Add(new ExaminationTypeConfiguration());
        }
    }
}