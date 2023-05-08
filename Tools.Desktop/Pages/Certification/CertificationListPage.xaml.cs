﻿using System.Windows;
using System.Windows.Controls;
using Tools.Services.ExaminationNatureServices;
using Tools.Services.ExaminationReasonServices;
using Tools.Services.ExaminationServices;
using Tools.Services.ExaminationTypeService;

namespace Tools.Desktop.Pages
{
	public partial class CertificationListPage : Page
    {
        private readonly IExaminationNatureService _examinationNatureService;
        private readonly IExaminationReasonService _examinationReasonService;
        private readonly IExaminationTypeService _examinationTypeService;
        private readonly IExaminationService _examinationService;

        public CertificationListPage(IExaminationNatureService examinationNatureService,
            IExaminationReasonService examinationReasonService,
            IExaminationTypeService examinationTypeService,
            IExaminationService examinationService)
		{
            _examinationNatureService = examinationNatureService;
            _examinationReasonService = examinationReasonService;
            _examinationTypeService = examinationTypeService;
            _examinationService = examinationService;

			InitializeComponent();
		}

        private void addNewCertificateButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parent = Window.GetWindow(this) as MainWindow;
            parent.pagesFrame.Navigate(new EditCertificationPage(_examinationNatureService,
                _examinationReasonService,
                _examinationTypeService,
                _examinationService));
        }

        private void deleteSelectedCertificate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void editSelectedCertificate_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}