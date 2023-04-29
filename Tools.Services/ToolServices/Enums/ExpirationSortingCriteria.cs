using System;
using System.ComponentModel.DataAnnotations;
using Tools.Services.Display;

namespace Tools.Services.ToolServices.Enums
{
    public enum ExpirationSortingCriteria
    {
        [Display(Name = "Придатний")]
        NonExpired = 1,

        [Display(Name = "Непридатний")]
        Expired = 2,
    }

    public static class ExpirationSortingCriteriaDisplay
    {
        public static string GetDisplayName(ExpirationSortingCriteria criteria)
        {
            return DisplayService<ExpirationSortingCriteria>.GetDisplayName(criteria);
        }

        public static ExpirationSortingCriteria GetEnumFromDisplay(string displayName)
        {
            string[] names = GetDisplayNames();
            for (int i = 0; i < names.Length; i++)
            {
                if (names[i] == displayName)
                {
                    return (ExpirationSortingCriteria)(i + 1);
                }
            }
            return (ExpirationSortingCriteria)0;
        }

        public static string[] GetDisplayNames()
        {
            string[] names = Enum.GetNames(typeof(ExpirationSortingCriteria));
            for (int i = 0; i < names.Length; i++)
            {
                ExpirationSortingCriteria criteria = (ExpirationSortingCriteria)(i + 1);
                names[i] = DisplayService<ExpirationSortingCriteria>.GetDisplayName(criteria);
            }
            return names;
        }
    }
}