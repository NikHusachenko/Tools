﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using Tools.Common;
using Tools.Database.Entities;
using Tools.Database.Enums;
using Tools.Desktop.Windows;
using Tools.Services.DocumentServices;
using Tools.Services.ExaminationNatureServices;
using Tools.Services.ExaminationReasonServices;
using Tools.Services.ExaminationServices;
using Tools.Services.ExaminationTypeService;
using Tools.Services.OrganizationUnitServices;
using Tools.Services.Response;
using Tools.Services.ToolGroupServices;
using Tools.Services.ToolServices;
using Tools.Services.ToolServices.Helpers;
using Tools.Services.ToolServices.Models;
using Tools.Services.ToolSubgroupServices;

namespace Tools.Desktop.Pages
{
	public partial class EditEquipmentPage : Page
	{
		private readonly IToolGroupService _toolGroupService;
		private readonly IToolSubgroupService _toolSubgroupService;
		private readonly IToolService _toolService;
		private readonly IDocumentService _documentService;
		private readonly IOrganizationUnitService _organizationUnitService;
		private readonly IExaminationNatureService _examinationNatureService;
		private readonly IExaminationReasonService _examinationReasonService;
		private readonly IExaminationTypeService _examinationTypeService;
		private readonly IExaminationService _examinationService;

		public EditEquipmentPage(IToolGroupService toolGroupService,
			IToolSubgroupService toolSubgroupService,
			IToolService toolService,
			IDocumentService documentService,
			IOrganizationUnitService organizationUnitService,
			IExaminationNatureService examinationNatureService,
			IExaminationReasonService examinationReasonService,
			IExaminationTypeService examinationTypeService,
			IExaminationService examinationService)
		{
			_toolGroupService = toolGroupService;
			_toolSubgroupService = toolSubgroupService;
			_toolService = toolService;
			_documentService = documentService;
			_organizationUnitService = organizationUnitService;
			_examinationNatureService = examinationNatureService;
			_examinationReasonService = examinationReasonService;
			_examinationTypeService = examinationTypeService;
			_examinationService = examinationService;

			InitializeComponent();

			string[] registrationTypes = RegistrationTypeDisplay.GetDisplayNames();
			foreach (string type in registrationTypes)
			{
				equipmentRegistrationComboBox.Items.Add(type);
			}

            dateOfCreatingCalendar.SelectedDate = DateTime.Now;
            commissioningDateCalendar.SelectedDate = DateTime.Now;
        }

        private void showCatalogButton_Click(object sender, RoutedEventArgs e)
        {
			CatalogWindow catalogWindow = new CatalogWindow(_toolGroupService,
				_toolSubgroupService);

			catalogWindow.ShowDialog();
			var response = catalogWindow.DataContext as ResponseService<ToolSubgroupEntity>;

			if (catalogWindow.DataContext != null &&
				!response.IsError)
			{
				equipmentGroupTextBox.Text = response.Value.Group.Name;
				equipmentSubgroupTextBox.Text = response.Value.Name;
			}
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
			organizationUnitComboBox.SelectedIndex = -1;
			equipmentGroupTextBox.Text = string.Empty;
			equipmentSubgroupTextBox.Text = string.Empty;
			equipmentNameTextBox.Text = string.Empty;
			equipmentBrandTextBox.Text = string.Empty;
			equipmentRegistrationComboBox.SelectedIndex = -1;
			equipmentRegistrationNumberTextBox.Text = string.Empty;
			equipmentIntraFactoryNumberTextBox.Text = string.Empty;
			equipmentManufacturerTextBox.Text = string.Empty;
			equipmentFactoryNumberTextBox.Text = string.Empty;
			dateOfCreatingCalendar.SelectedDate = DateTime.Now;
			commissioningDateCalendar.SelectedDate = DateTime.Now;
            equipmentExpirationYearTextBox.Text = string.Empty;
        }

        private void exiteButton_Click(object sender, RoutedEventArgs e)
        {
			var parent = Window.GetWindow(this) as MainWindow;
			parent.pagesFrame.Navigate(new EquipmentPage(_toolGroupService, 
				_toolSubgroupService, 
				_toolService, 
				_documentService,
				_organizationUnitService,
				_examinationNatureService,
				_examinationReasonService,
				_examinationTypeService,
				_examinationService));
        }

        private void equipmentRegistrationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
			if (equipmentRegistrationComboBox.SelectedItem != null)
			{
				string item = equipmentRegistrationComboBox.SelectedItem as string;
				if (RegistrationTypeHelper.GetEnumAsStringFromDisplayName(item) == RegistrationType.NonRegister ||
					item == null)
				{
                    equipmentRegistrationNumberTextBox.IsEnabled = false;
					equipmentRegistrationNumberTextBox.Text = string.Empty;
                }
				else
				{
                    equipmentRegistrationNumberTextBox.IsEnabled = true;
                }
            }
        }

        private async void saveEquipmentButton_Click(object sender, RoutedEventArgs e)
        {
			if (!int.TryParse(equipmentExpirationYearTextBox.Text, out int years))
			{
				equipmentExpirationYearLabel.Foreground = Brushes.Red;

                var timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(3);
                timer.Tick += (s, args) =>
                {
                    equipmentExpirationYearLabel.Foreground = Brushes.Black;
                    timer.Stop();
                };
                timer.Start();
				return;
            }

			CreateToolEntityPostModel vm = new CreateToolEntityPostModel()
			{
				Brand = equipmentBrandTextBox.Text,
				CommissioningDate = commissioningDateCalendar.SelectedDate.ToString(),
				CreatingDate = dateOfCreatingCalendar.SelectedDate.ToString(),
				ExpirationYear = years,
				FactoryNumber = equipmentFactoryNumberTextBox.Text,
				Group = equipmentGroupTextBox.Text,
				IntraFactoryNumber = equipmentIntraFactoryNumberTextBox.Text,
				Manufacturer = equipmentManufacturerTextBox.Text,
				Name = equipmentNameTextBox.Text,
				OrganizationUnit = organizationUnitComboBox.SelectedItem as string,
				Registration = equipmentRegistrationComboBox.SelectedItem as string,
				RegistrationNumber = equipmentRegistrationNumberTextBox.Text,
				Subgroup = equipmentSubgroupTextBox.Text,
			};

			var validateResponse = await _toolService.ValidateBeforeCreating(vm);
			if (validateResponse.IsError)
			{
				cancelButton_Click(sender, e);
				return;
			}

			var createResponse = await _toolService.Create(validateResponse.Value);
			if (createResponse.IsError)
			{
				cancelButton_Click(sender, e);
				return;
			}

			MessageBox.Show(Messages.CREATED_SUCCESSFULY);
            cancelButton_Click(sender, e);
        }

        private async void organizationUnitComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            ICollection<OrganizationUnitEntity> units = await _organizationUnitService.GetAll();
            foreach (OrganizationUnitEntity unit in units)
            {
                organizationUnitComboBox.Items.Add(unit.Name);
            }
        }
    }
}
