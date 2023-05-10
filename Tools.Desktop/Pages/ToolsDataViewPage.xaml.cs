using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Tools.Database.Entities;
using Tools.Database.Enums;

namespace Tools.Desktop.Pages
{
    public partial class ToolsDataViewPage : Page
    {
        private readonly List<ToolEntity> _tools;
        private int _selectedIndex;

        public ToolsDataViewPage(ICollection<ToolEntity> tools)
        {
            _tools = tools.ToList();

            InitializeComponent();

            if (_tools.Count > 0)
            {
                _selectedIndex = 0;
                ShowSelectedItem();
            }
        }

        private void firstRecordButton_Click(object sender, RoutedEventArgs e)
        {
            if (_tools.Count > 0)
            {
                _selectedIndex = 0;
                ShowSelectedItem();
            }
        }

        private void previousRecordButton_Click(object sender, RoutedEventArgs e)
        {
            if (_tools.Count > 0 && _selectedIndex - 1 >= 0)
            {
                _selectedIndex--;
                ShowSelectedItem();
            }
        }

        private void nextRecordButton_Click(object sender, RoutedEventArgs e)
        {
            if (_tools.Count > _selectedIndex + 1)
            {
                _selectedIndex++;
                ShowSelectedItem();
            }
        }

        private void lastRecordButton_Click(object sender, RoutedEventArgs e)
        {
            if (_tools.Count > 0)
            {
                _selectedIndex = _tools.Count - 1;
                ShowSelectedItem();
            }
        }

        private void ShowSelectedItem()
        {
            if (_selectedIndex == 0)
            {
                firstRecordButton.IsEnabled = false;
                previousRecordButton.IsEnabled = false;
            }
            else
            {
                firstRecordButton.IsEnabled = true;
                previousRecordButton.IsEnabled = true;
            }

            if (_tools.Count > 0)
            {
                nextRecordButton.IsEnabled = true;
                lastRecordButton.IsEnabled = true;
            }

            if (_selectedIndex == _tools.Count - 1)
            {
                nextRecordButton.IsEnabled = false;
                lastRecordButton.IsEnabled = false;
            }

            organizationUnitTextBox.Text = _tools[_selectedIndex].OrganizationUnit.Name;
            equipmentGroupTextBox.Text = _tools[_selectedIndex].Subgroup.Group.Name;
            equipmentSubgroupTextBox.Text = _tools[_selectedIndex].Subgroup.Name;
            equipmentNameTextBox.Text = _tools[_selectedIndex].Name;
            equipmentBrandTextBox.Text = _tools[_selectedIndex].Brand;
            equipmentRegistrationTextBox.Text = RegistrationTypeDisplay.GetDisplayName(_tools[_selectedIndex].Registration);
            equipmentRegistrationNumberTextBox.Text = _tools[_selectedIndex].RegistrationNumber;
            equipmentIntraFactoryNumberTextBox.Text = _tools[_selectedIndex].IntraFactoryNumber;
            equipmentManufacturerTextBox.Text = _tools[_selectedIndex].Manufacturer;
            equipmentFactoryNumberTextBox.Text = _tools[_selectedIndex].FactoryNumber;
            dateOfCreatingTextBox.Text = _tools[_selectedIndex].CreatingDate.ToString("yyyy.MM.dd");
            commissioningDateTextBox.Text = _tools[_selectedIndex].CommissioningDate.ToString("yyyy.MM.dd");
            equipmentExpirationYearTextBox.Text = _tools[_selectedIndex].ExpirationYear.ToString();
        }
    }


}
