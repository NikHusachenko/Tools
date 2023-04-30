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
                    Name = "Вантажопідйомні крани",
                },
                new ToolGroupEntity()
                {
                    Name = "Котли парові та водогрійні",
                },
                new ToolGroupEntity()
                {
                    Name = "Трубопроводи пари та гарячої води",
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
                    Name = "Автомобільні крани",
                },
                new ToolSubgroupEntity()
                {
                    GroupFK = groupsRecords[0].Id,
                    Name = "Баштові крани"
                },
                new ToolSubgroupEntity()
                {
                    GroupFK = groupsRecords[0].Id,
                    Name = "Кабельні крани",
                },
                new ToolSubgroupEntity()
                {
                    GroupFK = groupsRecords[0].Id,
                    Name = "Козлові крани",
                },
                new ToolSubgroupEntity()
                {
                    GroupFK = groupsRecords[0].Id,
                    Name = "Щоглові крани",
                },
                new ToolSubgroupEntity()
                {
                    GroupFK = groupsRecords[0].Id,
                    Name = "Бруківочні крани",
                },
                new ToolSubgroupEntity()
                {
                    GroupFK = groupsRecords[0].Id,
                    Name = "Портальні крани",
                },
                new ToolSubgroupEntity()
                {
                    GroupFK = groupsRecords[0].Id,
                    Name = "Водогрійні та пароводогрійні котли",
                },
                new ToolSubgroupEntity()
                {
                    GroupFK = groupsRecords[1].Id,
                    Name = "Котли парові та рідинні, що працюють з високотемпературними органічними теплоносіями",
                },
                new ToolSubgroupEntity()
                {
                    GroupFK = groupsRecords[1].Id,
                    Name = "Котли пересувних та транспортабельних установок та енергопоїздів",
                },
                new ToolSubgroupEntity()
                {
                    GroupFK = groupsRecords[1].Id,
                    Name = "Котли-утилізатори",
                },
                new ToolSubgroupEntity()
                {
                    GroupFK = groupsRecords[1].Id,
                    Name = "Парові котли",
                },
                new ToolSubgroupEntity()
                {
                    GroupFK = groupsRecords[1].Id,
                    Name = "Трубопроводи пари та гарячої води в межах котла",
                },
                new ToolSubgroupEntity()
                {
                    GroupFK = groupsRecords[1].Id,
                    Name = "Енерготехнологічні котли",
                },
                new ToolSubgroupEntity()
                {
                    GroupFK = groupsRecords[2].Id,
                    Name = "Трубопроводи І категорії 1 групи",
                },
                new ToolSubgroupEntity()
                {
                    GroupFK = groupsRecords[2].Id,
                    Name = "Трубопроводи І категорії 2 групи",
                },
                new ToolSubgroupEntity()
                {
                    GroupFK = groupsRecords[2].Id,
                    Name = "Трубопроводи І категорії 3 групи",
                },
                new ToolSubgroupEntity()
                {
                    GroupFK = groupsRecords[2].Id,
                    Name = "Трубопроводи І категорії 4 групи",
                },
                new ToolSubgroupEntity()
                {
                    GroupFK = groupsRecords[2].Id,
                    Name = "Трубопроводи ІІ категорії 1 групи",
                },
                new ToolSubgroupEntity()
                {
                    GroupFK = groupsRecords[2].Id,
                    Name = "Трубопроводи ІІ категорії 2 групи"
                },
                new ToolSubgroupEntity()
                {
                    GroupFK = groupsRecords[2].Id,
                    Name = "Трубопроводи ІІІ категорії 1 групи"
                },
                new ToolSubgroupEntity()
                {
                    GroupFK = groupsRecords[2].Id,
                    Name = "Трубопроводи ІІІ категорії 2 групи"
                },
                new ToolSubgroupEntity()
                {
                    GroupFK = groupsRecords[2].Id,
                    Name = "Трубопроводи ІV категорії",
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