using System.Windows;
using System.Windows.Controls;

namespace Tools.Desktop.Pages
{
	/// <summary>
	/// Логика взаимодействия для EditCertificationPage.xaml
	/// </summary>
	public partial class EditCertificationPage : Page
	{
		public EditCertificationPage()
		{
			InitializeComponent();
		}
		private void planningDateBtn_Click(object sender, RoutedEventArgs e)
		{
		//	planningDatePicker.Visibility = Visibility.Visible;
		}
		private void planningDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
		{

		}
		private void actualDateBtn_Click(object sender, RoutedEventArgs e)
		{
		//	actualDatePicker.Visibility = Visibility.Visible;
		}
		private void actualDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
		{

		}
	}
}
