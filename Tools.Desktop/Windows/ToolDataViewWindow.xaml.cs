﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using Tools.Common;
using Tools.Database.Entities;
using Tools.Database.Enums;
using Tools.Services.OrganizationUnitServices;
using Tools.Services.Response;
using Tools.Services.ToolGroupServices;
using Tools.Services.ToolServices;
using Tools.Services.ToolServices.Models;
using Tools.Services.ToolSubgroupServices;

namespace Tools.Desktop.Windows
{
    public partial class ToolDataViewWindow : Window
    {
        private readonly IOrganizationUnitService _organizationUnitService;
        private readonly IToolGroupService _toolGroupService;
        private readonly IToolSubgroupService _toolSubgroupService;
        private readonly IToolService _toolService;

        private readonly ToolEntity _toolEntity;

        public ToolDataViewWindow(IOrganizationUnitService organizationUnitService,
            IToolGroupService toolGroupService,
            IToolSubgroupService toolSubgroupService,
            IToolService toolService,
            ToolEntity toolEntity)
        {
            _organizationUnitService = organizationUnitService;
            _toolGroupService = toolGroupService;
            _toolSubgroupService = toolSubgroupService;
            _toolService = toolService;
            _toolEntity = toolEntity;

            InitializeComponent();

            dateOfCreatingCalendar.SelectedDate = _toolEntity.CreatingDate;
            commissioningDateCalendar.SelectedDate = _toolEntity.CommissioningDate;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            organizationUnitComboBox.Items.Clear();
            ICollection<OrganizationUnitEntity> units = await _organizationUnitService.GetAll();
            foreach (OrganizationUnitEntity unit in units)
            {
                organizationUnitComboBox.Items.Add(unit.Name);
            }

            equipmentRegistrationComboBox.Items.Clear();
            string[] names = RegistrationTypeDisplay.GetDisplayNames();
            foreach (string name in names)
            {
                equipmentRegistrationComboBox.Items.Add(name);
            }

            organizationUnitComboBox.SelectedItem = _toolEntity.OrganizationUnit.Name;
            equipmentGroupTextBox.Text = _toolEntity.Subgroup.Group.Name;
            equipmentSubgroupTextBox.Text = _toolEntity.Subgroup.Name;
            equipmentNameTextBox.Text = _toolEntity.Name;
            equipmentBrandTextBox.Text = _toolEntity.Brand;
            equipmentRegistrationComboBox.SelectedItem = RegistrationTypeDisplay.GetDisplayName(_toolEntity.Registration);
            equipmentRegistrationNumberTextBox.Text = _toolEntity.RegistrationNumber;
            equipmentIntraFactoryNumberTextBox.Text = _toolEntity.IntraFactoryNumber;
            equipmentManufacturerTextBox.Text = _toolEntity.Manufacturer;
            equipmentFactoryNumberTextBox.Text = _toolEntity.FactoryNumber;
            dateOfCreatingCalendar.SelectedDate = _toolEntity.CreatingDate;
            commissioningDateCalendar.SelectedDate = _toolEntity.CommissioningDate;
            equipmentExpirationYearTextBox.Text = _toolEntity.ExpirationYear.ToString(); 
        }

        private void openCatalogButton_Click(object sender, RoutedEventArgs e)
        {
            CatalogWindow window = new CatalogWindow(_toolGroupService,
                _toolSubgroupService);
            window.ShowDialog();
            var response = window.DataContext as ResponseService<ToolSubgroupEntity>;

            if (window.DataContext != null &&
                !response.IsError)
            {
                equipmentGroupTextBox.Text = response.Value.Group.Name;
                equipmentSubgroupTextBox.Text = response.Value.Name;
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Window_Loaded(sender, e);
        }

        private void equipmentRegistrationComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            string registrationName = equipmentRegistrationComboBox.SelectedItem as string;
            if (registrationName == null)
            {
                return;
            }

            RegistrationType registration = RegistrationTypeDisplay.GetEnumFromDisplay(registrationName);
            if (registration == RegistrationType.NonRegister)
            {
                equipmentRegistrationNumberTextBox.Text = string.Empty;
                equipmentRegistrationNumberTextBox.IsEnabled = false;
            }
            else
            {
                equipmentRegistrationNumberTextBox.IsEnabled = true;
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var response = await _toolService.Delete(_toolEntity);
            if (response.IsError)
            {
                return;
            }

            MessageBox.Show(Messages.DELETED_SUCCESSFULY);
            this.Close();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
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

            UpdateToolPostModel vm = new UpdateToolPostModel()
            {
                Id = _toolEntity.Id,
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

            var validateResponse = await _toolService.ValidateBeforeUpdating(vm);
            if (validateResponse.IsError)
            {
                cancelButton_Click(sender, e);
                return;
            }

            var createResponse = await _toolService.Update(validateResponse.Value);
            if (createResponse.IsError)
            {
                cancelButton_Click(sender, e);
                return;
            }

            MessageBox.Show(Messages.DATA_UPDATED_SUCCESSFULY);
            cancelButton_Click(sender, e);
        }
    }
}