using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Tools.Database.Enums
{
    public enum ExaminationReasonType
    {
        [Display(Name = "Наступний огляд згідно з правилами")]
        NextInspectionInCccordanceWithTheRules = 1,

        [Display(Name = "Після більш ніж 12 місяців бездіяльності")]
        AfterMoreThan12MonthsOfInactivity = 2,

        [Display(Name = "Після демонтажу та встановлення в іншому місці")]
        AfterDismantlingAndInstallationInAnotherPlace = 3,

        [Display(Name = "Після закінчення розрахункового (нормативного) терміну експлуатації")]
        AtTheEndOfTheEstimatedServiceLife = 4,

        [Display(Name = "Після ремонту із застосуванням зварювання")]
        AfterRepairWithTheUseOfWelding = 5,

        [Display(Name = "Після вирівнювання опуклостей і вм'ятин")]
        AfterStraighteningBulgesAndDents = 6,

        [Display(Name = "Після заміни елементів трубопроводу")]
        AfterReplacingPipingElements = 7,

        [Display(Name = "Після заміни деталей")]
        AfterReplacingParts = 8,

        [Display(Name = "Після заміни елементів котла")]
        AfterReplacingTheBoilerElements = 9,

        [Display(Name = "Після аварії")]
        AfterTheCccident = 10,

        [Display(Name = "Після встановлення")]
        AfterInstallation = 11,

        [Display(Name = "На розсуд інспектора технагляду")]
        AtTheDiscretionOfTheInspectionInspector = 12,

        [Display(Name = "На розсуд інженера спеціалізованої організації")]
        AtTheDiscretionOfTheEngineerOfASpecializedOrganization = 13,

        [Display(Name = "На розсуд особи, відповідальної за справний технічний стан")]
        AtTheDiscretionOfThePersonResponsibleForTheCorrectTechnicalCondition
    }

    public static class ExaminationReasonDisplay
    {
        public static string GetDisplayName(ExaminationReasonType examinationReasonType)
        {
            Type type = examinationReasonType.GetType();
            FieldInfo fieldInfo = type.GetField(examinationReasonType.ToString());
            DisplayAttribute displayAttribute = fieldInfo.GetCustomAttribute<DisplayAttribute>();

            if (displayAttribute != null)
            {
                return displayAttribute.Name;
            }
            return examinationReasonType.ToString();
        }

        public static string[] GetDisplayNames()
        {
            string[] names = Enum.GetNames(typeof(ExaminationReasonType));
            for (int i = 0; i < names.Length; i++)
            {
                ExaminationReasonType examinationReasonType = (ExaminationReasonType)(i + 1);
                Type type = examinationReasonType.GetType();
                FieldInfo fieldInfo = type.GetField(examinationReasonType.ToString());
                DisplayAttribute displayAttribute = fieldInfo.GetCustomAttribute<DisplayAttribute>();

                if (displayAttribute != null)
                {
                    names[i] = displayAttribute.Name;
                }
            }
            return names;
        }
    }
}