using System.Windows;
using Tools.Desktop.Pages;
using Tools.Desktop.Windows.CreateWindows;
using Tools.Services.DocumentServices;
using Tools.Services.ToolGroupServices;
using Tools.Services.ToolServices;
using Tools.Services.ToolSubgroupServices;

namespace Tools.Desktop
{
    public partial class MainWindow : Window
    {
        private readonly IToolGroupService _toolGroupService;
        private readonly IToolSubgroupService _toolSubgroupService;
        private readonly IToolService _toolService;
        private readonly IDocumentService _documentService;

        private readonly EquipmentPage _equipmentPage;
        private readonly EditEquipmentPage _editEquipmentPage;
        private readonly CertificationLayoutPage _certificationLayoutPage;

        public MainWindow(IToolGroupService toolGroupService,
            IToolSubgroupService toolSubgroupService,
            IToolService toolService,
            IDocumentService documentService,
            EquipmentPage equipmentPage, 
            EditEquipmentPage editEquipmentPage, 
            CertificationLayoutPage certificationLayoutPage)
        {
            _toolGroupService = toolGroupService;
            _toolSubgroupService = toolSubgroupService;
            _toolService = toolService;
            _documentService = documentService;

            _equipmentPage = equipmentPage;
            _editEquipmentPage = editEquipmentPage;
            _certificationLayoutPage = certificationLayoutPage;

            InitializeComponent();
        }

        private void EquipmentMenuItem_Click(object sender, RoutedEventArgs e)
		{
            pagesFrame.Navigate(new EquipmentPage(_toolGroupService, 
                _toolSubgroupService, 
                _toolService, 
                _documentService));
        }

		private void AddEquipmentMenuItem_Click(object sender, RoutedEventArgs e)
		{
            pagesFrame.Navigate(_editEquipmentPage);
		}

		private void GraphicMenuItem_Click(object sender, RoutedEventArgs e)
		{
            pagesFrame.Navigate(_certificationLayoutPage);
		}

        private void CreateNewGroup_Click(object sender, RoutedEventArgs e)
        {
            CreateGroupWindow window = new CreateGroupWindow(_toolGroupService);
            window.ShowDialog();
        }

        private void CreateNewSubgroup_Click(object sender, RoutedEventArgs e)
        {
            CreateSubgroupWindow window = new CreateSubgroupWindow(_toolSubgroupService, 
                _toolGroupService);
            window.ShowDialog();
        }

        private void CreateNewExaminationNature_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CreateNewExaminationSeason_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CreateNewExaminationType_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
