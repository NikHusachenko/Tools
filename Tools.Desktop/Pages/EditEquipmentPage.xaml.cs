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
		private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			addEquipmentGrid.Visibility = Visibility.Collapsed;
			catalogEquipmentGrid.Visibility = Visibility.Visible;
			equipmentFrame.Navigate(new CatalogEquipmentPage(addEquipmentGrid));
		}

		//private void CalendarButton_Click(object sender, System.Windows.RoutedEventArgs e)
		//{
		//	datePicker.Visibility = Visibility.Visible;
		//}
		private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
		{
			// handle the selected date change event here
		}

		private void dateCreationPicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
		{

		}

		private void CalendarCreationButton_Click(object sender, RoutedEventArgs e)
		{
			dateCreationPicker.Visibility = Visibility.Visible;
		}

		private void dateImplementetionPicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
		{

		}

		private void CalendarImplementetionButton_Click(object sender, RoutedEventArgs e)
		{
			dateImplementetionPicker.Visibility = Visibility.Visible;
		}
	}
}
