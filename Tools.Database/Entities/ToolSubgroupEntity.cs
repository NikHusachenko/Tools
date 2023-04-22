using System.Collections.Generic;

namespace Tools.Database.Entities
{
    public class ToolSubgroupEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public long GroupFK { get; set; }
        public ToolGroupEntity Group { get; set; }

        public ICollection<ToolEntity> Tools { get; set; }
    }
}