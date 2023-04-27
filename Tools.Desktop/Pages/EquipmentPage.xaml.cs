using System;
using System.Windows;
using System.Windows.Controls;
using Tools.Database.Enums;
using Tools.Desktop.Windows;
using Tools.Services.ToolGroupServices;
using Tools.Services.ToolServices;
using Tools.Services.ToolSubgroupServices;

namespace Tools.Desktop.Pages
{
	public partial class EquipmentPage : Page
	{
		private readonly IToolGroupService _toolGroupService;
		private readonly IToolSubgroupService _toolSubgroupService;
		private readonly IToolService _toolService;

		public EquipmentPage(IToolGroupService toolGroupService,
			IToolSubgroupService toolSubgroupService,
			IToolService toolService)
		{
			_toolGroupService = toolGroupService;
			_toolSubgroupService = toolSubgroupService;
			_toolService = toolService;

			InitializeComponent(); 

			equipmentFrame.Navigate(new EquipmentListPage());
			sortingComboBox.ItemsSource = Enum.GetNames(typeof(SortType));	
		}
		private void AddNewCard_Click(object sender, RoutedEventArgs e)
		{
			equipmentGrid.Visibility = Visibility.Hidden;
			pagesFrame.Navigate(new EditEquipmentPage(_toolGroupService,
				_toolSubgroupService,
				_toolService));
		}

		private void TechnicalCertification_Click(object sender, RoutedEventArgs e)
		{
			equipmentGrid.Visibility = Visibility.Hidden;
			pagesFrame.Navigate(new CertificationCardPage());
		}

		private void ShowSelectedCard_Click(object sender, RoutedEventArgs e)
		{
			equipmentGrid.Visibility = Visibility.Hidden;
			pagesFrame.Navigate(new EditEquipmentPage(_toolGroupService,
				_toolSubgroupService,
				_toolService));
		}
	}
}
