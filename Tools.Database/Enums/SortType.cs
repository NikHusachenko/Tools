using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Tools.Database.Enums
{
	public enum SortType
	{
		[Display(Name = "Немає")]
		None = 1,

		[Display(Name = "Сортувати по реєстрації")]
		SortByRegistration = 2,

		[Display(Name = "Сортуват за підрозділами")]
		SortByDivision = 3,

		[Display(Name = "Сортувати по групах")]
		SortByGroup = 4,

		[Display(Name = "Сортувати за строком використання")]
		SortByExpiration = 5,
	}

	public static class SortTypeDisplay
	{
		public static string GetDisplayName(SortType sortType)
		{
			Type type = sortType.GetType();
			FieldInfo fieldInfo = type.GetField(sortType.ToString());
			DisplayAttribute displayAttribute = fieldInfo.GetCustomAttribute<DisplayAttribute>();

			if (displayAttribute != null)
			{
				return displayAttribute.Name;
			}
			return sortType.ToString();
		}

		public static string[] GetDisplayNames()
		{
			string[] names = Enum.GetNames(typeof(SortType));
			for (int i = 0; i < names.Length; i++)
			{
				SortType sortType = (SortType)(i + 1);
				Type type = sortType.GetType();
				FieldInfo fieldInfo = type.GetField(sortType.ToString());
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
