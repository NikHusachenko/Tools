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

		private void planningCalendarBtn_Click(object sender, RoutedEventArgs e)
		{
			if (planningDatePicker.Visibility == Visibility.Visible)
			{
				planningDatePicker.Visibility = Visibility.Collapsed;
			}
			else
			{
				planningDatePicker.Visibility = Visibility.Visible;
			}
		}

		private void actualCalendarBtn_Click(object sender, RoutedEventArgs e)
		{
			if (actualDatePicker.Visibility == Visibility.Visible)
			{
				actualDatePicker.Visibility = Visibility.Collapsed;
			}
			else
			{
				actualDatePicker.Visibility = Visibility.Visible;
			}
		}
	}
}
