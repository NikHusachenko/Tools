using Tools.Database.Enums;

namespace Tools.Services.ToolServices.Helpers
{
    public static class OrganizationUnitHelper
    {
        public static OrganizationalUnitType GetEnumAsStringFromDisplayName(string organizationUnit)
        {
            string[] names = OrganizationalUnitDisplay.GetDisplayNames();
            for(int i = 0; i < names.Length; i++)
            {
                if (names[i] == organizationUnit)
                {
                    return (OrganizationalUnitType)(i + 1);
                }
            }
            return 0;
        }
    }
}