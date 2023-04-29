using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Tools.Services.Display
{
    public static class DisplayService<T> where T : Enum
    {
        public static string GetDisplayName(T value)
        {
            Type type = value.GetType();
            FieldInfo fieldInfo = type.GetField(value.ToString());
            DisplayAttribute displayAttribute = fieldInfo.GetCustomAttribute<DisplayAttribute>();

            if (displayAttribute != null)
            {
                return displayAttribute.Name;
            }
            return value.ToString();
        }
    }
}