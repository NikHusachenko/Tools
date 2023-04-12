using System.Windows;
using Tools.Desktop.EquipmentForms;
using Tools.Desktop.EquipmentForms.Pages;
using Tools.Desktop.Pages;

namespace Tools.Desktop
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

		private void EquipmentMenuItem_Click(object sender, RoutedEventArgs e)
		{
            pagesFrame.Navigate(new EquipmentPage());
        }

		private void AddEquipmentMenuItem_Click(object sender, RoutedEventArgs e)
		{
            pagesFrame.Navigate(new AddEquipmentPage());
		}

		private void GraphicMenuItem_Click(object sender, RoutedEventArgs e)
		{
            pagesFrame.Navigate(new GraphicEquipmentPage());
		}
	}
}
