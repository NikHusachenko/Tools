using System.Windows;
using System.Windows.Controls;
using Tools.Services.DocumentServices;
using Tools.Services.ExaminationNatureServices;
using Tools.Services.ExaminationReasonServices;
using Tools.Services.ExaminationServices;
using Tools.Services.ExaminationTypeService;
using Tools.Services.OrganizationUnitServices;
using Tools.Services.ToolGroupServices;
using Tools.Services.ToolServices;
using Tools.Services.ToolServices.Models;
using Tools.Services.ToolSubgroupServices;

namespace Tools.Desktop.Pages
{
	public partial class CertificationCardPage : Page
	{
		private readonly IToolGroupService _toolGroupService;
		private readonly IToolSubgroupService _toolSubgroupService;
		private readonly IToolService _toolService;
		private readonly IDocumentService _documentService;
		private readonly IOrganizationUnitService _organizationUnitService;
		private readonly IExaminationNatureService _examinationNatureService;
		private readonly IExaminationReasonService _examinationReasonService;
		private readonly IExaminationTypeService _examinationTypeService;
		private readonly IExaminationService _examinationService;

        private readonly ToolsPostModel _model;

		public CertificationCardPage(IToolGroupService toolGroupService,
			IToolSubgroupService toolSubgroupService,
			IToolService toolService,
			IDocumentService documentService,
			IOrganizationUnitService organizationUnitService,
			IExaminationNatureService examinationNatureService,
			IExaminationReasonService examinationReasonService,
			IExaminationTypeService examinationTypeService,
			IExaminationService examinationService,
			ToolsPostModel model)
		{
			_toolGroupService = toolGroupService;
			_toolSubgroupService = toolSubgroupService;
			_toolService = toolService;
			_documentService = documentService;
			_examinationNatureService = examinationNatureService;
			_examinationReasonService = examinationReasonService;
			_examinationTypeService = examinationTypeService;
			_examinationService = examinationService;

			_model = model;

			InitializeComponent();

			equipmentNameTextBox.Text = _model.Name;
			equipmentBrandTextBox.Text = _model.Brand;
			equipmentUnitTextBox.Text = _model.OranizationUnit;
		}

		private MainWindow GetParentWindow()
		{
			MainWindow main = Window.GetWindow(this) as MainWindow;
			return main;
		}

        private void backToEquipmentPage_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = GetParentWindow();
            main.pagesFrame.Navigate(new EquipmentPage(_toolGroupService,
                _toolSubgroupService,
                _toolService,
                _documentService,
				_organizationUnitService,
				_examinationNatureService,
				_examinationReasonService,
				_examinationTypeService,
				_examinationService));
        }

        private void createToolCertificate_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = GetParentWindow();
            main.pagesFrame.Navigate(new CertificationListPage(_examinationNatureService,
				_examinationReasonService,
				_examinationTypeService,
				_examinationService,
				_model.Id));
        }

    }
}
