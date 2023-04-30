using System.Windows;
using System.Windows.Controls;
using Tools.Services.ToolServices.Models;

namespace Tools.Desktop.Pages
{
	public partial class CertificationCardPage : Page
	{
		private readonly ToolsPostModel _model;

		public CertificationCardPage(ToolsPostModel model)
		{
			_model = model;

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
