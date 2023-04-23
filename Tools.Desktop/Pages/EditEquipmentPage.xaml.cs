using System;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Controls;
using Tools.Database.Enums;

namespace Tools.Desktop.Pages
{
	public partial class EditEquipmentPage : Page
	{
		public EditEquipmentPage()
		{
			InitializeComponent();

			string[] unitDisplayTypes = OrganizationalUnitDisplay.GetDisplayNames();
			foreach (string unit in unitDisplayTypes)
			{
				organizationUnitComboBox.Items.Add(unit);
			}
		}

		private void CreatingCalendarBtn_Click(object sender, RoutedEventArgs e)
		{
			/*if (dateCreatingPicker.Visibility == Visibility.Visible)
			{
				dateCreatingPicker.Visibility = Visibility.Collapsed;
			}
			else
			{
				dateCreatingPicker.Visibility = Visibility.Visible;
			}*/
		}

		private void ImplementationBtn_Click(object sender, RoutedEventArgs e)
		{
			/*if (dateImplementationPicker.Visibility == Visibility.Visible)
			{
				dateImplementationPicker.Visibility = Visibility.Collapsed;
			}
			else
			{
				dateImplementationPicker.Visibility = Visibility.Visible;
			}*/
		}

		private void catalogBtn_Click(object sender, RoutedEventArgs e)
		{
/*			editEquipmentGrid.Visibility = Visibility.Hidden;
			catalogEquipmentGrid.Visibility = Visibility.Visible;
			catalogEquipmentFrame.Navigate(new CatalogEquipmentPage(editEquipmentGrid));*/
		}
	}
}
