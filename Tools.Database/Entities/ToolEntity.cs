using Tools.Database.Enums;

namespace Tools.Database.Entities
{
    public class ToolEntity
    {
        public long Id { get; set; }
        public OrganizationalUnitType OrganizationalType { get; set; }

        public long SubgroupFK { get; set; }
        public ToolSubgroupEntity Subgroup { get; set; }
    }
}