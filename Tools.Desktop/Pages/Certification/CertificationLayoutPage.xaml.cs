using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Controls;
using Tools.Database.Entities;
using Tools.Services.DocumentServices;
using Tools.Services.ExaminationTypeService;
using Tools.Services.OrganizationUnitServices;
using Tools.Services.ToolSubgroupServices;

namespace Tools.Desktop.Pages
{
	public partial class CertificationLayoutPage : Page
	{
        private readonly IToolSubgroupService _toolSubgroupService;
        private readonly IOrganizationUnitService _organizationUnitService;
        private readonly IExaminationTypeService _examinationTypeService;
        private readonly IDocumentService _documentService;

        private readonly SemaphoreSlim _semaphoreSlim;

		public CertificationLayoutPage(IToolSubgroupService toolSubgroupService,
            IOrganizationUnitService organizationUnitService,
            IExaminationTypeService examinationTypeService,
            IDocumentService documentService)
		{
            _toolSubgroupService = toolSubgroupService;
            _organizationUnitService = organizationUnitService;
            _examinationTypeService = examinationTypeService;
            _documentService = documentService;

            _semaphoreSlim = new SemaphoreSlim(1);

			InitializeComponent();
		}

        private async void futureAll_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            await _semaphoreSlim.WaitAsync();

            dateContainer.Visibility = System.Windows.Visibility.Visible;
            unitContainer.Visibility = System.Windows.Visibility.Hidden;
            examinationTypeContainer.Visibility = System.Windows.Visibility.Hidden;

            dateFromDatePicker.SelectedDate = DateTime.Now;

            _semaphoreSlim.Release();
        }

        private async void FutureUnit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            await _semaphoreSlim.WaitAsync();

            dateContainer.Visibility = System.Windows.Visibility.Visible;
            unitContainer.Visibility = System.Windows.Visibility.Visible;
            examinationTypeContainer.Visibility = System.Windows.Visibility.Hidden;

            unitComboBox.Items.Clear();
            ICollection<OrganizationUnitEntity> units = await _organizationUnitService.GetAll();
            foreach (OrganizationUnitEntity unit in units)
            {
                unitComboBox.Items.Add(unit.Name);
            }
            dateFromDatePicker.SelectedDate = DateTime.Now;

            _semaphoreSlim.Release();
        }

        private async void fututreSubgroup_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            await _semaphoreSlim.WaitAsync();

            dateContainer.Visibility = System.Windows.Visibility.Visible;
            unitContainer.Visibility = System.Windows.Visibility.Hidden;
            examinationTypeContainer.Visibility = System.Windows.Visibility.Visible;

            bottomLabel.Content = "Підгрупа обладнання";

            BottomComboBox.Items.Clear();
            ICollection<ToolSubgroupEntity> subgroups = await _toolSubgroupService.GetAll();
            foreach (ToolSubgroupEntity subgroup in subgroups)
            {
                BottomComboBox.Items.Add(subgroup.Name);
            }
            dateFromDatePicker.SelectedDate = DateTime.Now;

            _semaphoreSlim.Release();
        }

        private async void futureType_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            await _semaphoreSlim.WaitAsync();

            dateContainer.Visibility = System.Windows.Visibility.Visible;
            unitContainer.Visibility = System.Windows.Visibility.Hidden;
            examinationTypeContainer.Visibility = System.Windows.Visibility.Visible;

            bottomLabel.Content = "Вид технічного огляду";

            BottomComboBox.Items.Clear();
            ICollection<ExaminationTypeEntity> types = await _examinationTypeService.GetAll();
            foreach (ExaminationTypeEntity type in types)
            {
                BottomComboBox.Items.Add(type.Name);
            }
            dateFromDatePicker.SelectedDate = DateTime.Now;

            _semaphoreSlim.Release();
        }

        private async void expiredAll_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            await _semaphoreSlim.WaitAsync();

            dateContainer.Visibility = System.Windows.Visibility.Hidden;
            unitContainer.Visibility = System.Windows.Visibility.Hidden;
            examinationTypeContainer.Visibility = System.Windows.Visibility.Hidden;

            _semaphoreSlim.Release();
        }

        private async void expiredUnit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            await _semaphoreSlim.WaitAsync();

            dateContainer.Visibility = System.Windows.Visibility.Hidden;
            unitContainer.Visibility = System.Windows.Visibility.Visible;
            examinationTypeContainer.Visibility = System.Windows.Visibility.Hidden;

            unitComboBox.Items.Clear();
            ICollection<OrganizationUnitEntity> units = await _organizationUnitService.GetAll();
            foreach (OrganizationUnitEntity unit in units)
            {
                unitComboBox.Items.Add(unit.Name);
            }

            _semaphoreSlim.Release();
        }

        private async void expiredSubgroup_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            await _semaphoreSlim.WaitAsync();

            dateContainer.Visibility = System.Windows.Visibility.Hidden;
            unitContainer.Visibility = System.Windows.Visibility.Hidden;
            examinationTypeContainer.Visibility = System.Windows.Visibility.Visible;

            bottomLabel.Content = "Підгрупа обладнання";
            BottomComboBox.Items.Clear();
            ICollection<ToolSubgroupEntity> subgroups = await _toolSubgroupService.GetAll();
            foreach (ToolSubgroupEntity subgroup in subgroups)
            {
                BottomComboBox.Items.Add(subgroup.Name);
            }

            _semaphoreSlim.Release();
        }

        private async void expiredType_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            await _semaphoreSlim.WaitAsync();

            dateContainer.Visibility = System.Windows.Visibility.Hidden;
            unitContainer.Visibility = System.Windows.Visibility.Hidden;
            examinationTypeContainer.Visibility = System.Windows.Visibility.Visible;

            bottomLabel.Content = "Вид технічного огляду";

            BottomComboBox.Items.Clear();
            ICollection<ExaminationTypeEntity> types = await _examinationTypeService.GetAll();
            foreach (ExaminationTypeEntity type in types)
            {
                BottomComboBox.Items.Add(type.Name);
            }

            _semaphoreSlim.Release();
        }
    }
}
