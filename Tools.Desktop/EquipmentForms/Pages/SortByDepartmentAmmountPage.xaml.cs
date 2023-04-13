using System.Collections.Generic;
using System.Windows.Controls;

namespace Tools.Desktop.EquipmentForms.Pages
{
	/// <summary>
	/// Логика взаимодействия для SortByDepartmentAmmountPage.xaml
	/// </summary>
	public partial class SortByDepartmentAmmountPage : Page
	{
		public SortByDepartmentAmmountPage()
		{
			List<string> list = new List<string>();
				InitializeComponent();
			for(int i=0; i < 10; i++)
			{
				list.Add("fdsfdsfdfdf"+i.ToString());
			}
			equipListView.ItemsSource= list;
		}
	}
}
