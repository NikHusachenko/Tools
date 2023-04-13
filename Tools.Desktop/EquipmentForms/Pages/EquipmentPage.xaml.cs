using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tools.Desktop.EquipmentForms.Pages
{
	/// <summary>
	/// Логика взаимодействия для EquipmentPage.xaml
	/// </summary>
	public partial class EquipmentPage : Page
	{
		
		public EquipmentPage()
		{
			InitializeComponent();
			equipmentFrame.Navigate(new EquipmentListViewPage());
		}

		private void SortByRegistrationButton_Click(object sender, RoutedEventArgs e)
		{
		    equipmentFrame.Navigate(new SortByRegistrationPage());
        }

		private void SortByDepartmentButton_Click(object sender, RoutedEventArgs e)
		{
			equipmentFrame.Navigate(new SortByDepartmentPage());
        }

		private void SortByGroupAmmountButton_Click(object sender, RoutedEventArgs e)
		{
			equipmentFrame.Navigate(new SortByGroupPage());
		}

		private void SortByDepartmentAmmountButton_Click(object sender, RoutedEventArgs e)
		{
			equipmentFrame.Navigate(new SortByDepartmentAmmountPage());
        }

		private void SortByExpiration_Click(object sender, RoutedEventArgs e)
		{
			equipmentFrame.Navigate(new SortByExpirationPage());
		}

		private void AddNewCard_Click(object sender, RoutedEventArgs e)
		{
			///Як відкрити іншу сторінку
		}
	}
}
