using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Tools.Database.Enums
{
    public enum OrganizationalUnitType
    {
        [Display(Name = "Автомобільна майстерня")]
        MotorVehicleWorkshop = 1,

        [Display(Name = "Механічна майстерня")]
        MechanicalWorkshop = 2,

        [Display(Name = "Склад готової продукції")]
        FinishedGoodsWarehouse = 3,

        [Display(Name = "Склад сировини")]
        RawMaterialWarehouse = 4,

        [Display(Name = "АЗС")]
        FuelStation = 5,

        [Display(Name = "Точка контролю доступу")]
        AccessControlPoint = 6,

        [Display(Name = "Медичний центр")]
        MedicalCenter = 7,

        [Display(Name = "Пожежна станція")]
        FireStation = 8,

        [Display(Name = "Технічне обслуговування")]
        MaintenanceService = 9,

        [Display(Name = "Управління")]
        Management = 10,
    }

    public static class OrganizationalUnitDisplay
    {
        public static string GetDisplayName(OrganizationalUnitType item)
        {
            FieldInfo field = item.GetType().GetField(item.ToString());
            var displayAttribute = field.GetCustomAttribute<DisplayAttribute>();

            if (displayAttribute != null)
            {
                return displayAttribute.Name;
            }
            return item.ToString();
        }

        public static OrganizationalUnitType GetEnumFromDisplay(string displayName)
        {
            string[] names = GetDisplayNames();
            for (int i = 0; i < names.Length; i++)
            {
                if (names[i] == displayName)
                {
                    return (OrganizationalUnitType)(i + 1);
                }
            }
            return (OrganizationalUnitType)0;
        }

        public static string[] GetDisplayNames()
        {
            string[] names = Enum.GetNames(typeof(OrganizationalUnitType));
            for (int i = 0; i < names.Length; i++)
            {
                OrganizationalUnitType type = (OrganizationalUnitType)(i + 1);
                FieldInfo field = type.GetType().GetField(type.ToString());
                var displayAttribute = field.GetCustomAttribute<DisplayAttribute>();

                if (displayAttribute != null)
                {
                    names[i] = displayAttribute.Name;
                }
            }
            return names;
        }
    }
}