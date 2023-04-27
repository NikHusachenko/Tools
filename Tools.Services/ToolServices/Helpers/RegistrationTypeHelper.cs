using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Tools.Database.Enums;

namespace Tools.Services.ToolServices.Helpers
{
    public static class RegistrationTypeHelper
    {
        public static RegistrationType GetEnumAsStringFromDisplayName(string displayName)
        {
            string[] names = RegistrationTypeDisplay.GetDisplayNames();
            for (int i = 0; i < names.Length; i++)
            {
                if (names[i] == displayName)
                {
                    return (RegistrationType)(i + 1);
                }
            }

            return (RegistrationType)0;
        }
    }
}