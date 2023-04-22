using System.Windows;
using Tools.Desktop.Pages;

namespace Tools.Desktop
{
    public partial class MainWindow : Window
    {
        private readonly EquipmentPage _equipmentPage;
        private readonly EditEquipmentPage _editEquipmentPage;
        private readonly CertificationLayoutPage _certificationLayoutPage;

        public MainWindow(EquipmentPage equipmentPage, EditEquipmentPage editEquipmentPage, CertificationLayoutPage certificationLayoutPage)
        {
            InitializeComponent();
            _equipmentPage = equipmentPage;
            _editEquipmentPage = editEquipmentPage;
            _certificationLayoutPage = certificationLayoutPage;
        }

        private void EquipmentMenuItem_Click(object sender, RoutedEventArgs e)
		{
            pagesFrame.Navigate(_equipmentPage);
        }

		private void AddEquipmentMenuItem_Click(object sender, RoutedEventArgs e)
		{
            pagesFrame.Navigate(_editEquipmentPage);
		}

		private void GraphicMenuItem_Click(object sender, RoutedEventArgs e)
		{
            pagesFrame.Navigate(_certificationLayoutPage);
		}
	}
}
