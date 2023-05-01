using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Tools.Database.Enums
{
    public enum ExaminationNatureType
    {
        [Display(Name = "Періодичний")]
        Periodic = 1,

        [Display(Name = "Не періодичний")]
        NotPeriodic = 2,
    }

    public class ExaminationNatureTypeDisplay
    {
        public static string GetDisplayName(ExaminationNatureType examinationNatureType)
        {
            Type type = examinationNatureType.GetType();
            FieldInfo fieldInfo = type.GetField(examinationNatureType.ToString());
            DisplayAttribute displayAttribute = fieldInfo.GetCustomAttribute<DisplayAttribute>();

            if (displayAttribute != null)
            {
                return displayAttribute.Name;
            }
            return examinationNatureType.ToString();
        }

        public static string[] GetDisplayNames()
        {
            string[] names = Enum.GetNames(typeof(ExaminationNatureType));
            for (int i = 0; i < names.Length; i++)
            {
                ExaminationNatureType examinationNatureType = (ExaminationNatureType)(i + 1);
                Type type = examinationNatureType.GetType();
                FieldInfo fieldInfo = type.GetField(examinationNatureType.ToString());
                DisplayAttribute displayAttribute = fieldInfo.GetCustomAttribute<DisplayAttribute>();

                if (displayAttribute != null)
                {
                    names[i] = displayAttribute.Name;
                }
            }
            return names;
        }

        public static ExaminationNatureType GetEnumFromString(string displayName)
        {
            string[] names = GetDisplayNames();
            for (int i = 0; i < names.Length; i++)
            {
                if (names[i] == displayName)
                {
                    return (ExaminationNatureType)(i + 1);
                }
            }
            return (ExaminationNatureType)0;
        }
    }
}