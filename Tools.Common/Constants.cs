using System.CodeDom;

namespace Tools.Common
{
	public class Errors
	{
		public const string NOT_FOUND_ERROR = "Не знайдено";
		public const string INVALID_VALUE_ERROR = "Невірно значення";
		public const string INVALID_DATE = "Невірна дата";
		public const string WAS_CREATED_ERROR = "Вже створено";
		public const string DELETE_GROUP_ERROR = "Неможливо видалити групу, якщо наявні підгрупи, що відносять до цієї групи. Каскадне видалення може призветсти до некоректної роботи програми";
		public const string DELETE_SUBGROUP_ERROR = "Неможливо видалити підгрупу, якщо наявні інструменти, які відносяться до цієї підгрупи. Каскадне видалення може призветсти до некоректної роботи програми";

    }

	public class Messages
	{
		public const string CREATED_SUCCESSFULY = "Створено успішно";
		public const string DELETED_SUCCESSFULY = "Видалено успішно";
		public const string REMANED_SUCCESSFULY = "Перейменовано успішно";
		public const string SAVED_SUCCESSFULY = "Збережено успішно";
		public const string CONFIRM_REMOVING = "Підтвердіть видалення";
	}
}
