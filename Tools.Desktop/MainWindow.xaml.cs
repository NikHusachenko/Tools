using System.Windows;
using Tools.Desktop.Pages;
using Tools.Desktop.Windows.CreateWindows;
using Tools.Services.DocumentServices;
using Tools.Services.ExaminationNatureServices;
using Tools.Services.ExaminationReasonServices;
using Tools.Services.ExaminationTypeService;
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
        private readonly IExaminationNatureService _examinationNatureService;
        private readonly IExaminationReasonService _examinationReasonService;
        private readonly IExaminationTypeService _examinationTypeService;

        private readonly EquipmentPage _equipmentPage;
        private readonly EditEquipmentPage _editEquipmentPage;
        private readonly CertificationLayoutPage _certificationLayoutPage;

        public MainWindow(IToolGroupService toolGroupService,
            IToolSubgroupService toolSubgroupService,
            IToolService toolService,
            IDocumentService documentService,
            IExaminationNatureService examinationNatureService,
            IExaminationReasonService examinationReasonService,
            IExaminationTypeService examinationTypeService,
            EquipmentPage equipmentPage, 
            EditEquipmentPage editEquipmentPage, 
            CertificationLayoutPage certificationLayoutPage)
        {
            _toolGroupService = toolGroupService;
            _toolSubgroupService = toolSubgroupService;
            _toolService = toolService;
            _documentService = documentService;
            _examinationNatureService = examinationNatureService;
            _examinationReasonService = examinationReasonService;
            _examinationTypeService = examinationTypeService;

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
            CreateExaminationNatureWindow window = new CreateExaminationNatureWindow(_examinationNatureService);
            window.ShowDialog();
        }

        private void CreateNewExaminationReason_Click(object sender, RoutedEventArgs e)
        {
            CreateExaminationSeasonWindow window = new CreateExaminationSeasonWindow(_examinationReasonService);
            window.ShowDialog();
        }

        private void CreateNewExaminationType_Click(object sender, RoutedEventArgs e)
        {
            CreateExaminationTypeWindow window = new CreateExaminationTypeWindow(_examinationTypeService);
            window.ShowDialog();
        }
    }
}
