using System.Windows;
using Tools.Desktop.EquipmentForms;
using Tools.Desktop.EquipmentForms.Pages;

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
          //  Equipment equipment = new Equipment();
          //  equipment.ShowDialog();
        }

		private void AddEquipmentMenuItem_Click(object sender, RoutedEventArgs e)
		{
            AddEquipment addEquipment= new AddEquipment();
            addEquipment.ShowDialog();
		}

		private void GraphicMenuItem_Click(object sender, RoutedEventArgs e)
		{
            GraphicOfEquipmentSurveys graphic = new GraphicOfEquipmentSurveys();
            graphic.ShowDialog();
		}
	}
}
