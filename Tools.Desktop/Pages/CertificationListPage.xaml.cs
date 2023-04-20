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

namespace Tools.Desktop.Pages
{
	/// <summary>
	/// Логика взаимодействия для CertificationListPage.xaml
	/// </summary>
	public partial class CertificationListPage : Page
	{
		public CertificationListPage()
		{
			InitializeComponent();
		}
		private void creatingCalendarBtn_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			if (creatingDatePicker.Visibility == Visibility.Visible)
			{
				creatingDatePicker.Visibility = Visibility.Collapsed;
			}
			else
			{
				creatingDatePicker.Visibility = Visibility.Visible;
			}
		}

		private void implementationCalendarBtn_Click(object sender, RoutedEventArgs e)
		{
			if (implementationDatePicker.Visibility == Visibility.Visible)
			{
				implementationDatePicker.Visibility = Visibility.Collapsed;
			}
			else
			{
				implementationDatePicker.Visibility = Visibility.Visible;
			}
		}
	}
}
