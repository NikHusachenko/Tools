using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using Tools.Common;
using Tools.Database.Entities;
using Tools.Desktop.Windows;
using Tools.Services.ExaminationNatureServices;
using Tools.Services.ExaminationReasonServices;
using Tools.Services.ExaminationServices;
using Tools.Services.ExaminationServices.Models;
using Tools.Services.ExaminationTypeService;
using Tools.Services.ToolServices;
using Tools.Services.ToolServices.Models;

namespace Tools.Desktop.Pages
{
	public partial class CertificationListPage : Page
    {
        private readonly IExaminationNatureService _examinationNatureService;
        private readonly IExaminationReasonService _examinationReasonService;
        private readonly IExaminationTypeService _examinationTypeService;
        private readonly IExaminationService _examinationService;
        private readonly IToolService _toolService;
        private readonly ToolsPostModel _toolsPostModel;

        private readonly SemaphoreSlim _semaphore;

        public CertificationListPage(IExaminationNatureService examinationNatureService,
            IExaminationReasonService examinationReasonService,
            IExaminationTypeService examinationTypeService,
            IExaminationService examinationService,
            IToolService toolService,
            ToolsPostModel toolsPostModel)
		{
            _examinationNatureService = examinationNatureService;
            _examinationReasonService = examinationReasonService;
            _examinationTypeService = examinationTypeService;
            _examinationService = examinationService;
            _toolService = toolService;
            _toolsPostModel = toolsPostModel;

            _semaphore = new SemaphoreSlim(1);

			InitializeComponent();
		}

        private void addNewCertificateButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parent = Window.GetWindow(this) as MainWindow;
            parent.pagesFrame.Navigate(new EditCertificationPage(_examinationNatureService,
                _examinationReasonService,
                _examinationTypeService,
                _examinationService,
                _toolService,
                _toolsPostModel));
        }

        private async void deleteSelectedCertificate_Click(object sender, RoutedEventArgs e)
        {
            ExaminationPostMode model = certificationsDataGrid.SelectedItem as ExaminationPostMode;
            if (model == null)
            {
                return;
            }

            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxResult result = MessageBox.Show(Messages.CONFIRM_REMOVING, "", button);

            if (result != MessageBoxResult.OK &&
                result != MessageBoxResult.Yes)
            {
                return;
            }

            var response = await _examinationService.Delete(model.Id);
            if (response.IsError)
            {
                return;
            }

            equipmentDataGrid_Loaded(sender, e);
        }

        private async void editSelectedCertificate_Click(object sender, RoutedEventArgs e)
        {
            ExaminationPostMode model = certificationsDataGrid.SelectedItem as ExaminationPostMode;
            if (model == null)
            {
                return;
            }

            CertificationDataViewWindow window = new CertificationDataViewWindow(_examinationNatureService,
                _examinationReasonService,
                _examinationTypeService,
                _examinationService,
                model.Id);
            window.ShowDialog();

            Page_Loaded(sender, e);
        }

        private async void equipmentDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            await _semaphore.WaitAsync();

            ICollection<ExaminationEntity> examinations = await _examinationService.GetByToolFK(_toolsPostModel.Id);
            ICollection<ExaminationPostMode> model = new List<ExaminationPostMode>();
            foreach (ExaminationEntity examination in examinations)
            {
                model.Add(new ExaminationPostMode()
                {
                    Id = examination.Id,
                    FactDate = examination.ActualExaminationDate,
                    Nature = examination.ExaminationNature.Name,
                    Reason = examination.ExaminationReason.Name,
                    ScheduleDate = examination.ScheduleExaminationDate,
                    Type = examination.ExaminationType.Name,
                });
            }
            certificationsDataGrid.ItemsSource = model;

            _semaphore.Release();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await _semaphore.WaitAsync();

            var response = await _toolService.GetById(_toolsPostModel.Id);
            if (response.IsError)
            {
                return;
            }

            equipmentBrandTextBox.Text = response.Value.Brand;
            equipmentCommissionDate.Text = response.Value.CommissioningDate.ToString("dd.MM.yyyy");
            equipmentCreateDate.Text = response.Value.CreatingDate.ToString("dd.MM.yyyy");
            equipmentNameTextBox.Text = response.Value.Name;
            equipmentUnitTextBox.Text = response.Value.OrganizationUnit.Name;

            _semaphore.Release();
        }

        private void certificationsDataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ExaminationPostMode model = certificationsDataGrid.SelectedItem as ExaminationPostMode;
            if (model == null)
            {
                return;
            }

            CertificationDataViewWindow window = new CertificationDataViewWindow(_examinationNatureService,
                _examinationReasonService,
                _examinationTypeService,
                _examinationService,
                model.Id);
            window.ShowDialog();

            equipmentDataGrid_Loaded(sender, e);
        }
    }
}