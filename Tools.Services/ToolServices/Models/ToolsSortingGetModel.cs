using System.Collections.Generic;
using Tools.Database.Entities;
using Tools.Database.Enums;
using Tools.Services.ToolServices.Enums;

namespace Tools.Services.ToolServices.Models
{
    public class ToolsSortingGetModel
    {
        public List<ToolsPostModel> Tools { get; set; }
        public RegistrationType Registration { get; set; }
        public OrganizationalUnitType OrganizationalUnit { get; set; }
        public string GroupName { get; set; }
        public string SubgroupName { get; set; }
        public ExpirationSortingCriteria ExpirationCriteria { get; set; }
    }
}