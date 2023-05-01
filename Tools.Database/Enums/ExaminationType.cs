using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Tools.Database.Enums
{
    public enum ExaminationType
    {
        [Display(Name = "Часткова періодична технічна перевірка")]
        PartialPeriodicTechnicalInspection = 1,

        [Display(Name = "Повний періодичний технічний огляд")]
        FullPeriodicTchnicalInspection = 2,

        [Display(Name = "Позачергова повна технічна перевірка")]
        ExtraordinaryFullTechnicalInspection = 3,

        [Display(Name = "Зовнішній огляд")]
        ExternalInspection = 4,

        [Display(Name = "Внутрішня перевірка")]
        InternalInspection = 5,

        [Display(Name = "Гідравлічне випробування")]
        HydraulicTesting = 6,

        [Display(Name = "Експертний огляд (Діагностування)")]
        ExpertExaminationReview = 7,
    }

    public static class ExaminationTypeDisplay
    {
        public static string GetDisplayName(ExaminationType examinationType)
        {
            Type type = examinationType.GetType();
            FieldInfo fieldInfo = type.GetField(examinationType.ToString());
            DisplayAttribute displayAttribute = fieldInfo.GetCustomAttribute<DisplayAttribute>();

            if (displayAttribute != null)
            {
                return displayAttribute.Name;
            }
            return examinationType.ToString();
        }

        public static string[] GetDisplayNames()
        {
            string[] names = Enum.GetNames(typeof(ExaminationType));
            for (int i = 0; i < names.Length; i++)
            {
                ExaminationType examinationType = (ExaminationType)(i + 1);
                Type type = examinationType.GetType();
                FieldInfo fieldInfo = type.GetField(examinationType.ToString());
                DisplayAttribute displayAttribute = fieldInfo.GetCustomAttribute<DisplayAttribute>();

                if (displayAttribute != null)
                {
                    names[i] = displayAttribute.Name;
                }
            }
            return names;
        }

        public static ExaminationType GetEnumFromString(string displayName)
        {
            string[] names = GetDisplayNames();
            for (int i = 0; i < names.Length; i++)
            {
                if (names[i] == displayName)
                {
                    return (ExaminationType)(i + 1);
                }
            }
            return (ExaminationType)0;
        }
    }
}