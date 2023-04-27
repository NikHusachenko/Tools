using Tools.Database.Entities;

namespace Tools.Services.ToolServices.Models
{
    public class CreateToolEntityPostModel
    {
        public string OrganizationUnit { get; set; }
        public string Group { get; set; }
        public string Subgroup { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Registration { get; set; }
        public string RegistrationNumber { get; set; }
        public string IntraFactoryNumber { get; set; }
        public string Manufacturer { get; set; }
        public string FactoryNumber { get; set; }
        public string CreatingDate { get; set; }
        public string CommissioningDate { get; set; }
        public int ExpirationYear { get; set; }
    }
}