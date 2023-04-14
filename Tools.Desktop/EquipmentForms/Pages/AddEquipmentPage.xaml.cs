using System.Windows.Controls;
using Tools.Desktop.EquipmentForms.Pages;

namespace Tools.Desktop.Pages
{
	/// <summary>
	/// Логика взаимодействия для AddEquipmentPage.xaml
	/// </summary>
	public partial class AddEquipmentPage : Page
	{
		public AddEquipmentPage()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			addEquipmentGrid.Visibility = System.Windows.Visibility.Collapsed;
			catalogEquipmentGrid.Visibility=System.Windows.Visibility.Visible;
			equipmentFrame.Navigate(new CatalogEquipmentPage(addEquipmentGrid));
        }
    }
}
