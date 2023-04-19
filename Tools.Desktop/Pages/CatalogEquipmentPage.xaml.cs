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

namespace Tools.Desktop.Pages
{
	/// <summary>
	/// Логика взаимодействия для CatalogEquipmentPage.xaml
	/// </summary>
	public partial class CatalogEquipmentPage : Page
	{
		private static Grid _editEquipmentGrid;
		public CatalogEquipmentPage(Grid editEquipmentGrid)
		{
			_editEquipmentGrid = editEquipmentGrid;
			InitializeComponent();
		}

		private void cancelBtn_Click(object sender, RoutedEventArgs e)
		{
			
			catalogGrid.Visibility = Visibility.Hidden;
			_editEquipmentGrid.Visibility = Visibility.Visible;
		}
	}
}
