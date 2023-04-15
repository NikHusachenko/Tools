using System.Windows;
using System.Windows.Controls;

namespace Tools.Desktop.Pages
{
	/// <summary>
	/// Логика взаимодействия для CertificationCardPage.xaml
	/// </summary>
	public partial class CertificationCardPage : Page
	{
		public CertificationCardPage()
		{
			InitializeComponent();
			certificationInfoFrame.Navigate(new CertificationMainInfoPage());
		}
		private void CreateAccountingCard_Click(object sender, RoutedEventArgs e)
		{
			accountingCardGrid.Visibility = Visibility.Hidden;
			pagesFram.Navigate(new EditCertificationPage());
		}

		private void backToEquipmentCatalog_Click(object sender, RoutedEventArgs e)
		{
			noDataGrid.Visibility = Visibility.Hidden;
			mainInfoFrame.Navigate(new CertificationListPage());
		}
	}
}
