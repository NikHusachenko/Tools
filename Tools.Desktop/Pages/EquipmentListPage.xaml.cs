using System.Collections.Generic;
using System.Windows.Controls;
using Tools.Services.ToolServices.Models;

namespace Tools.Desktop.Pages
{
	public partial class EquipmentListPage : Page
	{
		private readonly ICollection<ToolsPostModel> _vm;

		public EquipmentListPage(ICollection<ToolsPostModel> vm)
		{
			_vm = vm;

			InitializeComponent();
		}

        private void equipmentDataGrid_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
			equipmentDataGrid.ItemsSource = _vm;
        }
    }
}
