using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Tools.Database.Enums
{
    public enum RegistrationType
    {
        [Display(Name = "Підлягає реєстрації")]
        Register = 1,

        [Display(Name = "Не підлягає реєстрації")]
        NonRegister = 2,
    }

    public static class RegistrationTypeDisplay
    {
        public static string GetDisplayName(RegistrationType registrationType)
        {
            var type = registrationType.GetType();
            var fieldInfo = type.GetField(registrationType.ToString());
            var displayName = fieldInfo.GetCustomAttribute<DisplayAttribute>();

            if (displayName != null)
            {
                return displayName.Name;
            }
            return registrationType.ToString();
        }

        public static string[] GetDisplayNames()
        {
            string[] names = Enum.GetNames(typeof(RegistrationType));
            for (int i = 0; i < names.Length; i++)
            {
                RegistrationType registration = (RegistrationType)(i + 1);
                var type = registration.GetType();
                var fieldInfo = type.GetField(registration.ToString());
                var displayAttribute = fieldInfo.GetCustomAttribute<DisplayAttribute>();

                if (displayAttribute != null)
                {
                    names[i] = displayAttribute.Name;
                }
                else
                {
                    names[i] = registration.ToString();
                }
            }
            return names;
        }
    }
}