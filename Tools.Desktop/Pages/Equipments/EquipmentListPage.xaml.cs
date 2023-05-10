using System.Collections.Generic;
using System.Windows.Controls;
using Tools.Desktop.Windows;
using Tools.Services.OrganizationUnitServices;
using Tools.Services.ToolGroupServices;
using Tools.Services.ToolServices;
using Tools.Services.ToolServices.Models;
using Tools.Services.ToolSubgroupServices;

namespace Tools.Desktop.Pages
{
	public partial class EquipmentListPage : Page
	{
        private readonly IOrganizationUnitService _organizationUnitService;
        private readonly IToolGroupService _toolGroupService;
        private readonly IToolSubgroupService _toolSubgroupService;
        private readonly IToolService _toolService;
        private readonly ICollection<ToolsPostModel> _vm;

        private readonly EquipmentPage _parentObject;

		public EquipmentListPage(IOrganizationUnitService organizationUnitService,
            IToolGroupService toolGroupService,
            IToolSubgroupService toolSubgroupService,
            IToolService toolService,
			ICollection<ToolsPostModel> vm,
            EquipmentPage parentObject)
		{
            _organizationUnitService = organizationUnitService;
            _toolGroupService = toolGroupService;
            _toolSubgroupService = toolSubgroupService;
            _toolService = toolService;

            _vm = vm;
            _parentObject = parentObject;

			InitializeComponent();
		}

        private void equipmentDataGrid_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
			equipmentDataGrid.ItemsSource = _vm;
        }

        private async void equipmentDataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
			var item = equipmentDataGrid.SelectedItem as ToolsPostModel;
			var result = await _toolService.GetById(item.Id);

			if (!result.IsError)
			{
				ToolDataViewWindow toolDataViewWindow = new ToolDataViewWindow(_organizationUnitService,
                    _toolGroupService,
                    _toolSubgroupService,
                    _toolService,
                    result.Value);
				toolDataViewWindow.ShowDialog();

                _parentObject.UpdateFrame(sender, e);
			}
        }

        private void equipmentDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ToolsPostModel vm = equipmentDataGrid.SelectedItem as ToolsPostModel;
            DataContext = vm;
        }
    }
}
