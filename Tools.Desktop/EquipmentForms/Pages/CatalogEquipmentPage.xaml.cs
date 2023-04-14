using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tools.Desktop.EquipmentForms.Pages
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
