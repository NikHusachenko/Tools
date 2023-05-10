namespace Tools.Services.ToolServices.Models
{
    public class UpdateToolPostModel
    {
        public long Id { get; set; }
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