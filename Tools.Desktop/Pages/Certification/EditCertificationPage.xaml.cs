using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using Tools.Database.Entities;
using Tools.Services.ExaminationNatureServices;
using Tools.Services.ExaminationReasonServices;
using Tools.Services.ExaminationServices;
using Tools.Services.ExaminationTypeService;

namespace Tools.Desktop.Pages
{
	public partial class EditCertificationPage : Page
	{
        private readonly IExaminationNatureService _examinationNatureService;
        private readonly IExaminationReasonService _examinationReasonService;
        private readonly IExaminationTypeService _examinationTypeService;
        private readonly IExaminationService _examinationService;

        private readonly SemaphoreSlim _semaphore;

		public EditCertificationPage(IExaminationNatureService examinationNatureService,
            IExaminationReasonService examinationReasonService,
            IExaminationTypeService examinationTypeService,
            IExaminationService examinationService)
		{
            _examinationNatureService = examinationNatureService;
            _examinationReasonService = examinationReasonService;
            _examinationTypeService = examinationTypeService;
            _examinationService = examinationService;

            _semaphore = new SemaphoreSlim(1);

			InitializeComponent();
		}

        private async void examinationNatureComboBox_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            await _semaphore.WaitAsync();

            examinationNatureComboBox.Items.Clear();
            ICollection<ExaminationNatureEntity> natures = await _examinationNatureService.GetAll();
            foreach (ExaminationNatureEntity nature in natures)
            {
                examinationNatureComboBox.Items.Add(nature.Name);
            }

            _semaphore.Release();
        }

        private async void examinationReasonComboBox_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            await _semaphore.WaitAsync();

            examinationReasonComboBox.Items.Clear();
            ICollection<ExaminationReasonEntity> reasons = await _examinationReasonService.GetAll();
            foreach (ExaminationReasonEntity reason in reasons)
            {
                examinationReasonComboBox.Items.Add(reason.Name);
            }

            _semaphore.Release();
        }

        private async void examinationTypeComboBox_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            await _semaphore.WaitAsync();

            examinationTypeComboBox.Items.Clear();
            ICollection<ExaminationTypeEntity> types = await _examinationTypeService.GetAll();
            foreach (ExaminationTypeEntity type in types)
            {
                examinationTypeComboBox.Items.Add(type.Name);
            }

            _semaphore.Release();
        }

        private void exitButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainWindow parent = Window.GetWindow(this) as MainWindow;
            parent.pagesFrame.Navigate(new CertificationListPage(_examinationNatureService,
                _examinationReasonService,
                _examinationTypeService,
                _examinationService));
        }
    }
}