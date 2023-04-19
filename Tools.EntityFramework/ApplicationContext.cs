using Microsoft.EntityFrameworkCore;
using Tools.Common;

namespace Tools.EntityFramework
{
	public class ApplicationContext : DbContext
	{

		public ApplicationContext() 
		{
			Database.Migrate();
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(Configuration.DEFAULT_CONNECTION);
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			
		}
	}
}
