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

        public ApplicationDbContext() : base(Common.Configuration.DEFAULT_CONNECTION)
		{
            Database.CreateIfNotExists();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ToolConfiguration());
            modelBuilder.Configurations.Add(new ToolGroupConfiguration());
            modelBuilder.Configurations.Add(new ToolSubgroupConfiguration());
        }
    }
}