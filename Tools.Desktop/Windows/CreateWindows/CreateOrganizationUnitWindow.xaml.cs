using System.Collections.Generic;
using System.Windows;
using Tools.Common;
using Tools.Database.Entities;
using Tools.Services.OrganizationUnitServices;

namespace Tools.Desktop.Windows.CreateWindows
{
    public partial class CreateOrganizationUnitWindow : Window
    {
        private readonly IOrganizationUnitService _organizationUnitService;

        public CreateOrganizationUnitWindow(IOrganizationUnitService organizationUnitService)
        {
            _organizationUnitService = organizationUnitService;

            InitializeComponent();
        }

        private void selectUnitComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            string unitName = selectUnitComboBox.SelectedItem as string;
            if (unitName == null)
            {
                renameUnitTextBox.Text = string.Empty;
                unitControlContainer.Visibility = Visibility.Hidden;
                unitRenameContainer.Visibility = Visibility.Hidden;
                return;
            }

            renameUnitTextBox.Text = unitName;
            unitControlContainer.Visibility = Visibility.Visible;
            unitRenameContainer.Visibility = Visibility.Visible;
        }

        private async void selectUnitComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            selectUnitComboBox.Items.Clear();

            ICollection<OrganizationUnitEntity> units = await _organizationUnitService.GetAll();
            foreach (OrganizationUnitEntity unit in units)
            {
                selectUnitComboBox.Items.Add(unit.Name);
            }
            renameUnitTextBox.Text = string.Empty;
            unitControlContainer.Visibility = Visibility.Hidden;
            unitRenameContainer.Visibility = Visibility.Hidden;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string newUnitName = newUnitNameTextBox.Text;
            newUnitNameTextBox.Text = string.Empty;
            if (string.IsNullOrEmpty(newUnitName))
            {
                return;
            }

            var response = await _organizationUnitService.Create(newUnitName);
            if (response.IsError)
            {
                return;
            }

            MessageBox.Show(Messages.CREATED_SUCCESSFULY);
            selectUnitComboBox_Loaded(sender, e);
        }

        private async void deleteUnitButton_Click(object sender, RoutedEventArgs e)
        {
            string name = renameUnitTextBox.Text;
            renameUnitTextBox.Text = string.Empty;
            if (string.IsNullOrEmpty(name))
            {
                return;
            }

            var response = await _organizationUnitService.Delete(name);
            if (response.IsError)
            {
                return;
            }

            MessageBox.Show(Messages.DELETED_SUCCESSFULY);
            selectUnitComboBox_Loaded(sender, e);
        }

        private async void renameUnitButton_Click(object sender, RoutedEventArgs e)
        {
            string oldName = selectUnitComboBox.SelectedItem as string;
            if (oldName == null)
            {
                renameUnitTextBox.Text = string.Empty;
                return;
            }
            string newName = renameUnitTextBox.Text;

            var response = await _organizationUnitService.Rename(oldName, newName);
            if (response.IsError)
            {
                renameUnitTextBox.Text = string.Empty;
                return;
            }

            MessageBox.Show(Messages.REMANED_SUCCESSFULY);
            selectUnitComboBox_Loaded(sender, e);
        }
    }
}