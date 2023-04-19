using System.Windows;
using System.Windows.Controls;

namespace Tools.Desktop.Pages
{
	/// <summary>
	/// Логика взаимодействия для EditEquipmentPage.xaml
	/// </summary>
	public partial class EditEquipmentPage : Page
	{
		public EditEquipmentPage()
		{
			InitializeComponent();
		}

		private void CreatingCalendarBtn_Click(object sender, RoutedEventArgs e)
		{
			
			if (dateCreatingPicker.Visibility == Visibility.Visible)
			{
				dateCreatingPicker.Visibility = Visibility.Collapsed;
			}
			else
			{
				dateCreatingPicker.Visibility = Visibility.Visible;
			}
		}

		private void ImplementationBtn_Click(object sender, RoutedEventArgs e)
		{
			if (dateImplementationPicker.Visibility == Visibility.Visible)
			{
				dateImplementationPicker.Visibility = Visibility.Collapsed;
			}
			else
			{
				dateImplementationPicker.Visibility = Visibility.Visible;
			}
		}

		private void catalogBtn_Click(object sender, RoutedEventArgs e)
		{
			editEquipmentGrid.Visibility = Visibility.Hidden;
			catalogEquipmentGrid.Visibility = Visibility.Visible;
			catalogEquipmentFrame.Navigate(new CatalogEquipmentPage(editEquipmentGrid));
		}
	}
}
