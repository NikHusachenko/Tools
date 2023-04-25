using System.Windows.Controls;
using Tools.Database.Entities;
using Tools.Database.Enums;
using Tools.Desktop.Windows;
using Tools.Services.Response;
using Tools.Services.ToolGroupServices;
using Tools.Services.ToolSubgroupServices;

namespace Tools.Desktop.Pages
{
	public partial class EditEquipmentPage : Page
	{
		private readonly IToolGroupService _toolGroupService;
		private readonly IToolSubgroupService _toolSubgroupService;

		public EditEquipmentPage(IToolGroupService toolGroupService,
			IToolSubgroupService toolSubgroupService)
		{
			_toolGroupService = toolGroupService;
			_toolSubgroupService = toolSubgroupService;

			InitializeComponent();

            string[] unitDisplayTypes = OrganizationalUnitDisplay.GetDisplayNames();
			foreach (string unit in unitDisplayTypes)
			{
				organizationUnitComboBox.Items.Add(unit);
			}
		}

        private void showCatalogButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
			CatalogWindow catalogWindow = new CatalogWindow(_toolGroupService,
				_toolSubgroupService);

			catalogWindow.ShowDialog();
			var response = catalogWindow.DataContext as ResponseService<ToolSubgroupEntity>;

			if (!response.IsError)
			{
				equipmentGroupTextBox.Text = response.Value.Group.Name;
				equipmentSubgroupTextBox.Text = response.Value.Name;
			}
        }
    }
}
