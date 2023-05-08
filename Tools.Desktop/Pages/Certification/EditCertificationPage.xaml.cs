using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using Tools.Common;
using Tools.Database.Entities;
using Tools.Services.ExaminationNatureServices;
using Tools.Services.ExaminationReasonServices;
using Tools.Services.ExaminationServices;
using Tools.Services.ExaminationServices.Models;
using Tools.Services.ExaminationTypeService;
using Tools.Services.ToolServices;
using Tools.Services.ToolServices.Models;

namespace Tools.Desktop.Pages
{
	public partial class EditCertificationPage : Page
	{
        private readonly IExaminationNatureService _examinationNatureService;
        private readonly IExaminationReasonService _examinationReasonService;
        private readonly IExaminationTypeService _examinationTypeService;
        private readonly IExaminationService _examinationService;
        private readonly IToolService _toolService;

        private readonly ToolsPostModel _model;

        private readonly SemaphoreSlim _semaphore;

        public EditCertificationPage(IExaminationNatureService examinationNatureService,
            IExaminationReasonService examinationReasonService,
            IExaminationTypeService examinationTypeService,
            IExaminationService examinationService,
            IToolService toolService,
            ToolsPostModel model)
		{
            _examinationNatureService = examinationNatureService;
            _examinationReasonService = examinationReasonService;
            _examinationTypeService = examinationTypeService;
            _examinationService = examinationService;
            _toolService = toolService;
            _model = model;

            _semaphore = new SemaphoreSlim(1);

			InitializeComponent();
		}

        private async void examinationNatureComboBox_Loaded(object sender, RoutedEventArgs e)
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

        private async void examinationReasonComboBox_Loaded(object sender, RoutedEventArgs e)
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

        private async void examinationTypeComboBox_Loaded(object sender, RoutedEventArgs e)
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

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parent = Window.GetWindow(this) as MainWindow;
            parent.pagesFrame.Navigate(new CertificationListPage(_examinationNatureService,
                _examinationReasonService,
                _examinationTypeService,
                _examinationService,
                _toolService,
                _model));
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            examinationNatureComboBox.SelectedIndex = -1;
            examinationReasonComboBox.SelectedIndex = -1;
            examinationTypeComboBox.SelectedIndex = -1;

            scheduleExaminationDate.SelectedDate = DateTime.Now;
            factExaminationDate.SelectedDate = DateTime.Now;

            examinationResultTextBox.SelectAll();
            examinationResultTextBox.Selection.Text = string.Empty;
        }

        private async void saveExaminationbutton_Click(object sender, RoutedEventArgs e)
        {
            string nature = examinationNatureComboBox.SelectedItem as string;
            if (nature == null)
            {
                cancelButton_Click(sender, e);
                return;
            }

            string reason = examinationReasonComboBox.SelectedItem as string;
            if (reason == null)
            {
                cancelButton_Click(sender, e);
                return;
            }

            string type = examinationTypeComboBox.SelectedItem as string;
            if (type == null)
            {
                cancelButton_Click(sender, e);
                return;
            }

            if (!scheduleExaminationDate.SelectedDate.HasValue)
            {
                scheduleExaminationDate.SelectedDate = DateTime.Now;
                return;
            }
            DateTime scheduleDate = scheduleExaminationDate.SelectedDate.Value;

            if (!factExaminationDate.SelectedDate.HasValue)
            {
                factExaminationDate.SelectedDate= DateTime.Now;
                return;
            }
            DateTime factDate = factExaminationDate.SelectedDate.Value;

            examinationResultTextBox.SelectAll();
            string result = examinationResultTextBox.Selection.Text;

            CreateExaminationPostModel vm = new CreateExaminationPostModel()
            {
                ExaminationNatureName = nature,
                ExaminationReasonName = reason,
                ExaminationResult = result,
                ExaminationTypeName = type,
                FactDate = factDate,
                ScheduleDate = scheduleDate,
                ToolFK = _model.Id,
            };

            var response = await _examinationService.Create(vm);
            cancelButton_Click(sender, e);
            if (response.IsError)
            {
                return;
            }

            MessageBox.Show(Messages.CREATED_SUCCESSFULY);
        }
    }
}