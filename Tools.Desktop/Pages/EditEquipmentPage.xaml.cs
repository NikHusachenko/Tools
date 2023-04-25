using System.Windows;
using System.Windows.Controls;
using Tools.Database.Enums;

namespace Tools.Desktop.Pages
{
	public partial class EditEquipmentPage : Page
	{
        private bool _creatinDateCalendarIsFixed = false;

		public EditEquipmentPage()
		{
			InitializeComponent();

            string[] unitDisplayTypes = OrganizationalUnitDisplay.GetDisplayNames();
			foreach (string unit in unitDisplayTypes)
			{
				organizationUnitComboBox.Items.Add(unit);
			}
		}

        private void dateOfCreatingTextBox_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            dateOfCreatingCalendar.Visibility = Visibility.Visible;
        }

        private void dateOfCreatingTextBox_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            dateOfCreatingCalendar.Visibility = Visibility.Hidden;
        }

        private void dateOfCreatingTextBox_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _creatinDateCalendarIsFixed = true;
        }
    }
}
