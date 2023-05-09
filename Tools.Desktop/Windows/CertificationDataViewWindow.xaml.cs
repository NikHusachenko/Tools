using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Tools.Common;
using Tools.Database.Entities;
using Tools.Services.ExaminationNatureServices;
using Tools.Services.ExaminationReasonServices;
using Tools.Services.ExaminationServices;
using Tools.Services.ExaminationServices.Models;
using Tools.Services.ExaminationTypeService;

namespace Tools.Desktop.Windows
{
    public partial class CertificationDataViewWindow : Window
    {
        private readonly IExaminationNatureService _examinationNatureService;
        private readonly IExaminationReasonService _examinationReasonService;
        private readonly IExaminationTypeService _examinationTypeService;
        private readonly IExaminationService _examinationService;
        private readonly long _examinationId;

        private readonly SemaphoreSlim _semaphore;

        private ExaminationEntity _dbRecord;

        public CertificationDataViewWindow(IExaminationNatureService examinationNatureService,
            IExaminationReasonService examinationReasonService,
            IExaminationTypeService examinationTypeService,
            IExaminationService examinationService,
            long examinationId)
        {
            _examinationNatureService = examinationNatureService;
            _examinationReasonService = examinationReasonService;
            _examinationTypeService = examinationTypeService;
            _examinationService = examinationService;
            _examinationId = examinationId;

            _semaphore = new SemaphoreSlim(1);

            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var response = await _examinationService.GetById(_examinationId);
            if (response.IsError)
            {
                this.Close();
            }
            _dbRecord = response.Value;

            await SetDefaultEntityData();
        }

        private async void saveExaminationbutton_Click(object sender, RoutedEventArgs e)
        {
            UpdateExaminationPostModel model = GetDefaultEntityData();
            var response = await _examinationService.Update(model);

            if(!response.IsError)
            {
                MessageBox.Show(Messages.SAVED_SUCCESSFULY);
                _dbRecord = response.Value;
            }

            await SetDefaultEntityData();
        }

        private async Task SetDefaultEntityData()
        {
            examinationNatureComboBox.Items.Clear();
            ICollection<ExaminationNatureEntity> natures = await _examinationNatureService.GetAll();
            foreach (ExaminationNatureEntity nature in natures)
            {
                examinationNatureComboBox.Items.Add(nature.Name);
            }
            examinationNatureComboBox.SelectedItem = _dbRecord.ExaminationNature.Name;

            examinationReasonComboBox.Items.Clear();
            ICollection<ExaminationReasonEntity> reasons = await _examinationReasonService.GetAll();
            foreach (ExaminationReasonEntity reason in reasons)
            {
                examinationReasonComboBox.Items.Add(reason.Name);
            }
            examinationReasonComboBox.SelectedItem = _dbRecord.ExaminationReason.Name;

            examinationTypeComboBox.Items.Clear();
            ICollection<ExaminationTypeEntity> types = await _examinationTypeService.GetAll();
            foreach (ExaminationTypeEntity type in types)
            {
                examinationTypeComboBox.Items.Add(type.Name);
            }
            examinationTypeComboBox.SelectedItem = _dbRecord.ExaminationType.Name;

            scheduleExaminationDate.SelectedDate = _dbRecord.ScheduleExaminationDate;
            factExaminationDate.SelectedDate = _dbRecord.ActualExaminationDate;
            examinationResultTextBox.Selection.Text = _dbRecord.ExaminationResult;
        }

        private UpdateExaminationPostModel GetDefaultEntityData()
        {
            string nature = examinationNatureComboBox.SelectedItem as string;
            if (string.IsNullOrEmpty(nature))
            {
                return null;
            }

            string reason = examinationReasonComboBox.SelectedItem as string;
            if (string.IsNullOrEmpty(reason))
            {
                return null;
            }

            string type = examinationTypeComboBox.SelectedItem as string;
            if (string.IsNullOrEmpty(type))
            {
                return null;
            }

            DateTime scheduleDate = scheduleExaminationDate.SelectedDate.Value;
            DateTime factDate = factExaminationDate.SelectedDate.Value;

            examinationResultTextBox.SelectAll();
            string result = examinationResultTextBox.Selection.Text;

            return new UpdateExaminationPostModel()
            {
                Id = _examinationId,
                ExaminationNatureName = nature,
                ExaminationReasonName = reason,
                ExaminationResult = result,
                ExaminationTypeName = type,
                FactDate = factDate,
                ScheduleDate = scheduleDate,
            };
        }

        private async void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            await SetDefaultEntityData();
        }
    }
}