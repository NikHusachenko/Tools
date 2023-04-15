using System.Windows;
using System.Windows.Controls;

namespace Tools.Desktop.Pages
{
	/// <summary>
	/// Логика взаимодействия для CatalogEquipmentPage.xaml
	/// </summary>
	public partial class CatalogEquipmentPage : Page
	{
		private static Grid grid;
		public CatalogEquipmentPage(Grid addEquipmentGrid)
		{
			grid = addEquipmentGrid;
			InitializeComponent();
		}

		private void cancel_Click(object sender, RoutedEventArgs e)
		{
			grid.Visibility = Visibility.Visible;
			currentGrid.Visibility = Visibility.Hidden;
		}
	}
}
