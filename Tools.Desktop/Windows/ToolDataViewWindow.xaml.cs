using System.Windows;
using Tools.Database.Entities;
using Tools.Database.Enums;

namespace Tools.Desktop.Windows
{
    public partial class ToolDataViewWindow : Window
    {
        public ToolDataViewWindow(ToolEntity toolEntity)
        {
            InitializeComponent();

            organizationTextBox.Text = toolEntity.OrganizationUnit.Name;
            equipmentGroupTextBox.Text = toolEntity.Subgroup.Group.Name;
            equipmentSubgroupTextBox.Text = toolEntity.Subgroup.Name;
            equipmentNameTextBox.Text = toolEntity.Name;
            equipmentBrandTextBox.Text = toolEntity.Brand;
            equipmentRegistrationTextBox.Text = RegistrationTypeDisplay.GetDisplayName(toolEntity.Registration);
            equipmentRegistrationNumberTextBox.Text = toolEntity.RegistrationNumber;
            equipmentIntraFactoryNumberTextBox.Text = toolEntity.IntraFactoryNumber;
            equipmentManufacturerTextBox.Text = toolEntity.Manufacturer;
            equipmentFactoryNumberTextBox.Text = toolEntity.FactoryNumber;
            dateOfCreatingTextBox.Text = toolEntity.CreatingDate.ToString("dd.MM.yyyy");
            commissioningDateTextBox.Text = toolEntity.CommissioningDate.ToString("dd.MM.yyyy");
            equipmentExpirationYearTextBox.Text = toolEntity.ExpirationYear.ToString();
        }
    }
}