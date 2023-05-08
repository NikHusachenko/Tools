using System.Collections.Generic;
using System.Windows;
using Tools.Database.Entities;
using Tools.Services.ToolGroupServices;
using Tools.Services.ToolGroupServices.Models;
using Tools.Services.ToolSubgroupServices;

namespace Tools.Desktop.Windows
{
    public partial class CatalogWindow : Window
    {
        private readonly IToolGroupService _toolGroupService;
        private readonly IToolSubgroupService _toolSubgroupService;

        public CatalogWindow(IToolGroupService toolGroupService,
            IToolSubgroupService toolSubgroupService)
        {
            _toolGroupService = toolGroupService;
            _toolSubgroupService = toolSubgroupService;

            InitializeComponent();
        }

        private async void groupCatalogGrid_Loaded(object sender, RoutedEventArgs e)
        {
            var result = await _toolGroupService.GetAll();
            var data = new List<CatalogGetModel>();

            int counter = 0;
            foreach (ToolGroupEntity group in result)
            {
                foreach (ToolSubgroupEntity subgroup in group.Subgroups)
                {
                    data.Add(new CatalogGetModel()
                    {
                        Number = ++counter,
                        SubgroupName = subgroup.Name,
                        GroupName = group.Name
                    });
                }
            }

            groupCatalogGrid.ItemsSource = data;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            CatalogGetModel selectedData = groupCatalogGrid.SelectedItem as CatalogGetModel;
            DataContext = await _toolSubgroupService.GetByName(selectedData.SubgroupName);
            this.Close();
        }

        private async void groupCatalogGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            CatalogGetModel selectedData = groupCatalogGrid.SelectedItem as CatalogGetModel;
            if (selectedData != null)
            {
                DataContext = await _toolSubgroupService.GetByName(selectedData.SubgroupName);
            }
            this.Close();
        }
    }
}