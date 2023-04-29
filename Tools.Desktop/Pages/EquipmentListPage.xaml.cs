using System.Collections.Generic;
using System.Windows.Controls;
using Tools.Desktop.Windows;
using Tools.Services.ToolServices;
using Tools.Services.ToolServices.Models;

namespace Tools.Desktop.Pages
{
	public partial class EquipmentListPage : Page
	{
        private readonly IToolService _toolService;
        private readonly ICollection<ToolsPostModel> _vm;

		public EquipmentListPage(IToolService toolService,
			ICollection<ToolsPostModel> vm)
		{
            _toolService = toolService;

            _vm = vm;

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
				ToolDataViewWindow toolDataViewWindow = new ToolDataViewWindow(result.Value);
				toolDataViewWindow.ShowDialog();
			}
        }
    }
}
