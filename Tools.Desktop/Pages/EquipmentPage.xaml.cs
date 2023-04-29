﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Tools.Database.Entities;
using Tools.Database.Enums;
using Tools.Services.Response;
using Tools.Services.ToolGroupServices;
using Tools.Services.ToolServices;
using Tools.Services.ToolServices.Enums;
using Tools.Services.ToolServices.Models;
using Tools.Services.ToolSubgroupServices;

namespace Tools.Desktop.Pages
{
	public partial class EquipmentPage : Page
	{
		private readonly IToolGroupService _toolGroupService;
		private readonly IToolSubgroupService _toolSubgroupService;
		private readonly IToolService _toolService;

		private SemaphoreSlim _semaphore;

		public EquipmentPage(IToolGroupService toolGroupService,
			IToolSubgroupService toolSubgroupService,
			IToolService toolService)
		{
			_toolGroupService = toolGroupService;
			_toolSubgroupService = toolSubgroupService;
			_toolService = toolService;

			_semaphore = new SemaphoreSlim(1);

			InitializeComponent();
		}
		private void AddNewCard_Click(object sender, RoutedEventArgs e)
		{
            MainWindow parent = GetParentWindow();
			parent.pagesFrame.Navigate(new EditEquipmentPage(_toolGroupService,
                _toolSubgroupService,
                _toolService));
		}

		private void TechnicalCertification_Click(object sender, RoutedEventArgs e)
		{
            MainWindow parent = GetParentWindow();
            parent.pagesFrame.Navigate(new CertificationCardPage());
		}

		private void ShowSelectedCard_Click(object sender, RoutedEventArgs e)
		{
			MainWindow parent = GetParentWindow();
			parent.pagesFrame.Navigate(new EditEquipmentPage(_toolGroupService,
				_toolSubgroupService,
				_toolService));
		}

		private MainWindow GetParentWindow()
		{
			MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
			return mainWindow;
		}

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
			await _semaphore.WaitAsync();

            string[] registrations = RegistrationTypeDisplay.GetDisplayNames();
            foreach (string registration in registrations) registrationSortingComboBox.Items.Add(registration);

			string[] units = OrganizationalUnitDisplay.GetDisplayNames();
			foreach (string unit in units) unitSortingComboBox.Items.Add(unit);

			ICollection<ToolGroupEntity> groups = await _toolGroupService.GetAll();
			foreach (ToolGroupEntity group in groups) groupSortingComboBox.Items.Add(group.Name);

			ICollection<ToolSubgroupEntity> subgroups = await _toolSubgroupService.GetAll();
			foreach (ToolSubgroupEntity subgroup in subgroups) subgroupSortingComboBox.Items.Add(subgroup.Name);

			string[] expirationNames = ExpirationSortingCriteriaDisplay.GetDisplayNames();
			foreach (string name in expirationNames) expirationSortingComboBox.Items.Add(name);

			_semaphore.Release();
        }

        private async void registrationSortingComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
			var response = await AssemblePostModel();
			if (response.IsError)
			{
				MessageBox.Show(response.ErrorMessage);
				return;
			}

			ToolsSortingGetModel vm = await _toolService.Sorting(response.Value);
            equipmentFrame.Navigate(new EquipmentListPage(vm.Tools));
        }

		private async Task<ResponseService<ToolsSortingPostModel>> AssemblePostModel()
		{
            var groupResult = await _toolGroupService.GetByName(groupSortingComboBox.SelectedItem as string);
			var subgroupResult = await _toolSubgroupService.GetByName(subgroupSortingComboBox.SelectedItem as string);

            ToolsSortingPostModel vm = new ToolsSortingPostModel()
            {
                ExpirationCriteria = ExpirationSortingCriteriaDisplay.GetEnumFromDisplay(expirationSortingComboBox.SelectedItem as string),
                GroupName = groupResult.Value?.Name,
				OrganizationalUnit = OrganizationalUnitDisplay.GetEnumFromDisplay(unitSortingComboBox.SelectedItem as string),
				Registration = RegistrationTypeDisplay.GetEnumFromDisplay(registrationSortingComboBox.SelectedItem as string),
				SubgroupName = subgroupResult.Value?.Name,
            };

			return ResponseService<ToolsSortingPostModel>.Ok(vm);
        }

        private async void equipmentFrame_Loaded(object sender, RoutedEventArgs e)
        {
			await _semaphore.WaitAsync();

			ToolsSortingGetModel vm = await _toolService.Sorting(new ToolsSortingPostModel());
			equipmentFrame.Navigate(new EquipmentListPage(vm.Tools));

			_semaphore.Release();
        }
    }
}
