using System.Collections.Generic;

namespace Tools.Database.Entities
{
    public class ToolGroupEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public ICollection<ToolSubgroupEntity> Subgroups { get; set; }
    }
}