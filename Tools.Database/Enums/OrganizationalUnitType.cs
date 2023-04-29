using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Tools.Database.Enums
{
    public enum OrganizationalUnitType
    {
        [Display(Name = "Motor Vehicle Workshop")]
        MotorVehicleWorkshop = 1,

        [Display(Name = "Mechanical Workshop")]
        MechanicalWorkshop = 2,

        [Display(Name = "Finished Goods Warehouse")]
        FinishedGoodsWarehouse = 3,

        [Display(Name = "Raw Material Warehouse")]
        RawMaterialWarehouse = 4,

        [Display(Name = "Fuel Station")]
        FuelStation = 5,

        [Display(Name = "Access Control Point")]
        AccessControlPoint = 6,

        [Display(Name = "Medical Center")]
        MedicalCenter = 7,

        [Display(Name = "Fire Station")]
        FireStation = 8,

        [Display(Name = "Maintenance Service")]
        MaintenanceService = 9,

        [Display(Name = "Management")]
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