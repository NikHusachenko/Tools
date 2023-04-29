using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Tools.Database.Entities;

namespace Tools.EntityFramework
{
    public class ApplicationContextInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            base.Seed(context);
        }

        public async static Task InitializeDefaultData(ApplicationDbContext context)
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

            for (int i = 0; i < groupsRecords.Count; i++)
            {
                string groupName = groupsRecords[i].Name;
                ToolGroupEntity dbRecord = await groups.FirstOrDefaultAsync(group => group.Name == groupName);
                if (dbRecord == null)
                {
                    groups.Add(groupsRecords[i]);
                    await context.SaveChangesAsync();
                }
                else
                {
                    groupsRecords[i].Id = dbRecord.Id;
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

            for (int i = 0; i < subgroupsRecords.Count; i++)
            {
                string subgroupName = subgroupsRecords[i].Name;
                ToolSubgroupEntity dbRecord = await subgroups.FirstOrDefaultAsync(subgroup => subgroup.Name == subgroupName);
                if (dbRecord == null)
                {
                    subgroups.Add(subgroupsRecords[i]);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}