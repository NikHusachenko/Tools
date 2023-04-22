using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Tools.Database.Entities;

namespace Tools.EntityFramework
{
    public class ApplicationContextInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            base.Seed(context);
        }

        public async static void InitializeDefaultData(ApplicationDbContext context)
        {
            var groups = context.Set<ToolGroupEntity>();
            var groupsRecords = new List<ToolGroupEntity>
            {
                new ToolGroupEntity()
                {
                    Name = "Lifting cranes",
                },
                new ToolGroupEntity()
                {
                    Name = "Steam and hot water boilers",
                },
                new ToolGroupEntity()
                {
                    Name = "Steam and hot water pipelines",
                }
            };

            foreach (var record in groups)
            {
                var dbRecord = await groups.FirstOrDefaultAsync(group => group.Name == record.Name);
                if (record == null)
                {
                    groups.Add(record);
                    await context.SaveChangesAsync();
                }
            }

            var subgroups = context.Set<ToolSubgroupEntity>();
            var subgroupsRecords = new List<ToolSubgroupEntity>
            {
                new ToolSubgroupEntity()
                {
                    GroupFK = groupsRecords[0].Id,
                    Name = "Mobile cranes",
                },
                new ToolSubgroupEntity()
                {
                    GroupFK = groupsRecords[0].Id,
                    Name = "Tower cranes"
                },
                new ToolSubgroupEntity()
                {
                    GroupFK = groupsRecords[0].Id,
                    Name = "Cable cranes",
                },
                new ToolSubgroupEntity()
                {
                    GroupFK = groupsRecords[0].Id,
                    Name = "Gantry cranes",
                },
                new ToolSubgroupEntity()
                {
                    GroupFK = groupsRecords[0].Id,
                    Name = "Mast cranes",
                },
                new ToolSubgroupEntity()
                {
                    GroupFK = groupsRecords[0].Id,
                    Name = "Bridge cranes",
                },
                new ToolSubgroupEntity()
                {
                    GroupFK = groupsRecords[0].Id,
                    Name = "Portal cranes",
                },
                new ToolSubgroupEntity()
                {
                    GroupFK = groupsRecords[0].Id,
                    Name = "Hot water and steam boilers",
                },
                new ToolSubgroupEntity()
                {
                    GroupFK = groupsRecords[1].Id,
                    Name = "Steam and liquid boilers operating with high-temperature organic heat carriers",
                },
                new ToolSubgroupEntity()
                {
                    GroupFK = groupsRecords[1].Id,
                    Name = "Boilers for mobile and transportable installations and power trains",
                },
                new ToolSubgroupEntity()
                {
                    GroupFK = groupsRecords[1].Id,
                    Name = "Waste heat boilers",
                },
                new ToolSubgroupEntity()
                {
                    GroupFK = groupsRecords[1].Id,
                    Name = "Steam boilers",
                },
                new ToolSubgroupEntity()
                {
                    GroupFK = groupsRecords[1].Id,
                    Name = "Steam and hot water pipelines within the boiler",
                },
                new ToolSubgroupEntity()
                {
                    GroupFK = groupsRecords[1].Id,
                    Name = "Energy technology boilers",
                },
                new ToolSubgroupEntity()
                {
                    GroupFK = groupsRecords[2].Id,
                    Name = "Pipelines of category I, group 1",
                },
                new ToolSubgroupEntity()
                {
                    GroupFK = groupsRecords[2].Id,
                    Name = "Pipelines of category I, group 2",
                },
                new ToolSubgroupEntity()
                {
                    GroupFK = groupsRecords[2].Id,
                    Name = "Pipelines of category I, group 3",
                },
                new ToolSubgroupEntity()
                {
                    GroupFK = groupsRecords[2].Id,
                    Name = "Pipelines of category I, group 4",
                },
                new ToolSubgroupEntity()
                {
                    GroupFK = groupsRecords[2].Id,
                    Name = "Pipelines of category II, group 1",
                },
                new ToolSubgroupEntity()
                {
                    GroupFK = groupsRecords[2].Id,
                    Name = "Pipelines of category II, group 2"
                },
                new ToolSubgroupEntity()
                {
                    GroupFK = groupsRecords[2].Id,
                    Name = "Pipelines of category III, group 1"
                },
                new ToolSubgroupEntity()
                {
                    GroupFK = groupsRecords[2].Id,
                    Name = "Pipelines of category III, group 2"
                },
                new ToolSubgroupEntity()
                {
                    GroupFK = groupsRecords[2].Id,
                    Name = "Pipelines of category IV",
                },
            };

            foreach (var record in subgroupsRecords)
            {
                var dbRecord = await subgroups.FirstOrDefaultAsync(subgroup => subgroup.Name == record.Name);
                if (dbRecord == null)
                {
                    subgroups.Add(record);
                    await context.SaveChangesAsync();
                }
            }

            subgroups.AddRange(subgroupsRecords);
            await context.SaveChangesAsync();
        }
    }
}