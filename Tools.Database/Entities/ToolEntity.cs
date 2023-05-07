using System;
using System.Collections.Generic;
using Tools.Database.Enums;

namespace Tools.Database.Entities
{
    public class ToolEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public RegistrationType Registration { get; set; }
        public string RegistrationNumber { get; set; }
        public string IntraFactoryNumber { get; set; }
        public string FactoryNumber { get; set; }
        public string Manufacturer { get; set; }
        public DateTime CreatingDate { get; set; }
        public DateTime CommissioningDate { get; set; }
        public int ExpirationYear { get; set; }

        public long SubgroupFK { get; set; }
        public ToolSubgroupEntity Subgroup { get; set; }

        public long OrganizationUnitFK { get; set; }
        public OrganizationUnitEntity OrganizationUnit { get; set; }

        public ICollection<ExaminationEntity> Examinutions { get; set; }
    }
}