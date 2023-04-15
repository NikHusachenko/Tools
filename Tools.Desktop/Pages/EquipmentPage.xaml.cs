using System.Windows;
using System.Windows.Controls;

namespace Tools.Desktop.Pages
{
	/// <summary>
	/// Логика взаимодействия для EquipmentPage.xaml
	/// </summary>
	public partial class EquipmentPage : Page
	{
		public EquipmentPage()
		{
			InitializeComponent(); 
			equipmentFrame.Navigate(new EquipmentListPage());
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
			equipmentGrid.Visibility = Visibility.Hidden;
			pagesFrame.Navigate(new EditEquipmentPage());
			//Open AddEquipmmentPage
		}

		private void TechnicalCertification_Click(object sender, RoutedEventArgs e)
		{
			equipmentGrid.Visibility = Visibility.Hidden;
			pagesFrame.Navigate(new CertificationCardPage());
		}

		private void ShowSelectedCard_Click(object sender, RoutedEventArgs e)
		{
			equipmentGrid.Visibility = Visibility.Hidden;
			pagesFrame.Navigate(new EditEquipmentPage());
		}
	}
}
