using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Controls;
using Tools.Database.Entities;
using Tools.Services.DocumentServices;
using Tools.Services.ExaminationServices;
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
        private readonly IExaminationService _examinationService;
        private readonly IDocumentService _documentService;

        private readonly SemaphoreSlim _semaphoreSlim;

        private ICollection<ExaminationEntity> _examinations;
        private DateTime _selectedDateFrom;
        private OrganizationUnitEntity _selectedUnit;
        private ToolSubgroupEntity _subgroup;
        private ExaminationTypeEntity _examinationType;
        private string _title;
        private CertificationSorting _sorting;

		public CertificationLayoutPage(IToolSubgroupService toolSubgroupService,
            IOrganizationUnitService organizationUnitService,
            IExaminationTypeService examinationTypeService,
            IExaminationService examinationService,
            IDocumentService documentService)
		{
            _toolSubgroupService = toolSubgroupService;
            _organizationUnitService = organizationUnitService;
            _examinationTypeService = examinationTypeService;
            _examinationService = examinationService;
            _documentService = documentService;

            _semaphoreSlim = new SemaphoreSlim(1);

            _sorting = CertificationSorting.FutureAll;

			InitializeComponent();
		}

        private async void futureAll_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            await _semaphoreSlim.WaitAsync();

            dateContainer.Visibility = System.Windows.Visibility.Visible;
            unitContainer.Visibility = System.Windows.Visibility.Hidden;
            examinationSubgroupContainer.Visibility = System.Windows.Visibility.Hidden;
            examinationTypeContainer.Visibility = System.Windows.Visibility.Hidden;

            dateFromDatePicker.SelectedDate = DateTime.Now;
            _sorting = CertificationSorting.FutureAll;

            _semaphoreSlim.Release();
        }

        private async void FutureUnit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            await _semaphoreSlim.WaitAsync();

            dateContainer.Visibility = System.Windows.Visibility.Visible;
            unitContainer.Visibility = System.Windows.Visibility.Visible;
            examinationSubgroupContainer.Visibility = System.Windows.Visibility.Hidden;
            examinationTypeContainer.Visibility = System.Windows.Visibility.Hidden;

            unitComboBox.Items.Clear();
            ICollection<OrganizationUnitEntity> units = await _organizationUnitService.GetAll();
            foreach (OrganizationUnitEntity unit in units)
            {
                unitComboBox.Items.Add(unit.Name);
            }
            dateFromDatePicker.SelectedDate = DateTime.Now;
            _sorting = CertificationSorting.FutureByUnit;

            _semaphoreSlim.Release();
        }

        private async void fututreSubgroup_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            await _semaphoreSlim.WaitAsync();

            dateContainer.Visibility = System.Windows.Visibility.Visible;
            unitContainer.Visibility = System.Windows.Visibility.Hidden;
            examinationSubgroupContainer.Visibility = System.Windows.Visibility.Visible;
            examinationTypeContainer.Visibility = System.Windows.Visibility.Hidden;

            subgroupComboBox.Items.Clear();
            ICollection<ToolSubgroupEntity> subgroups = await _toolSubgroupService.GetAll();
            foreach (ToolSubgroupEntity subgroup in subgroups)
            {
                subgroupComboBox.Items.Add(subgroup.Name);
            }
            dateFromDatePicker.SelectedDate = DateTime.Now;
            _sorting = CertificationSorting.FutureBySubgroup;

            _semaphoreSlim.Release();
        }

        private async void futureType_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            await _semaphoreSlim.WaitAsync();

            dateContainer.Visibility = System.Windows.Visibility.Visible;
            unitContainer.Visibility = System.Windows.Visibility.Hidden;
            examinationSubgroupContainer.Visibility = System.Windows.Visibility.Hidden;
            examinationTypeContainer.Visibility = System.Windows.Visibility.Visible;

            examinationTypeComboBox.Items.Clear();
            ICollection<ExaminationTypeEntity> types = await _examinationTypeService.GetAll();
            foreach (ExaminationTypeEntity type in types)
            {
                examinationTypeComboBox.Items.Add(type.Name);
            }
            dateFromDatePicker.SelectedDate = DateTime.Now;
            _sorting = CertificationSorting.FutureByExaminationType;

            _semaphoreSlim.Release();
        }

        private async void expiredAll_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            await _semaphoreSlim.WaitAsync();

            dateContainer.Visibility = System.Windows.Visibility.Hidden;
            unitContainer.Visibility = System.Windows.Visibility.Hidden;
            examinationSubgroupContainer.Visibility = System.Windows.Visibility.Hidden;
            examinationTypeContainer.Visibility = System.Windows.Visibility.Hidden;

            _sorting = CertificationSorting.ExpiredAll;

            _semaphoreSlim.Release();
        }

        private async void expiredUnit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            await _semaphoreSlim.WaitAsync();

            dateContainer.Visibility = System.Windows.Visibility.Hidden;
            unitContainer.Visibility = System.Windows.Visibility.Visible;
            examinationSubgroupContainer.Visibility = System.Windows.Visibility.Hidden;
            examinationTypeContainer.Visibility = System.Windows.Visibility.Hidden;

            unitComboBox.Items.Clear();
            ICollection<OrganizationUnitEntity> units = await _organizationUnitService.GetAll();
            foreach (OrganizationUnitEntity unit in units)
            {
                unitComboBox.Items.Add(unit.Name);
            }
            _sorting = CertificationSorting.ExpiredByUnit;

            _semaphoreSlim.Release();
        }

        private async void expiredSubgroup_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            await _semaphoreSlim.WaitAsync();

            dateContainer.Visibility = System.Windows.Visibility.Hidden;
            unitContainer.Visibility = System.Windows.Visibility.Hidden;
            examinationSubgroupContainer.Visibility = System.Windows.Visibility.Visible;
            examinationTypeContainer.Visibility = System.Windows.Visibility.Hidden;

            subgroupComboBox.Items.Clear();
            ICollection<ToolSubgroupEntity> subgroups = await _toolSubgroupService.GetAll();
            foreach (ToolSubgroupEntity subgroup in subgroups)
            {
                subgroupComboBox.Items.Add(subgroup.Name);
            }
            _sorting = CertificationSorting.ExpiredBySubgroup;

            _semaphoreSlim.Release();
        }

        private async void expiredType_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            await _semaphoreSlim.WaitAsync();

            dateContainer.Visibility = System.Windows.Visibility.Hidden;
            unitContainer.Visibility = System.Windows.Visibility.Hidden;
            examinationSubgroupContainer.Visibility = System.Windows.Visibility.Hidden;
            examinationTypeContainer.Visibility = System.Windows.Visibility.Visible;

            examinationTypeComboBox.Items.Clear();
            ICollection<ExaminationTypeEntity> types = await _examinationTypeService.GetAll();
            foreach (ExaminationTypeEntity type in types)
            {
                examinationTypeComboBox.Items.Add(type.Name);
            }
            _sorting = CertificationSorting.ExpiredByExaminationType;

            _semaphoreSlim.Release();
        }

        private void dateFromDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedDateFrom = dateFromDatePicker.SelectedDate.Value;
        }

        private async void unitComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string unitName = unitComboBox.SelectedItem as string;
            if (unitName == null)
            {
                _selectedUnit = null;
                return;
            }

            var response = await _organizationUnitService.GetByName(unitName);
            if (response.IsError)
            {
                _selectedUnit = null;
                return;
            }

            _selectedUnit = response.Value;
        }

        private async void subgroupComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string subgroup = subgroupComboBox.SelectedItem as string;
            if (subgroup == null)
            {
                _subgroup = null;
                return;
            }

            var response = await _toolSubgroupService.GetByName(subgroup);
            if (response.IsError)
            {
                _subgroup = null;
                return;
            }

            _subgroup = response.Value;
        }

        private async void examinationTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string type = examinationTypeComboBox.SelectedItem as string;
            if (type == null)
            {
                _examinationType = null;
                return;
            }

            var response = await _examinationTypeService.GetByName(type);
            if (response.IsError)
            {
                _examinationType = null;
                return;
            }

            _examinationType = response.Value;
        }

        private async void PrintButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            switch (_sorting)
            {
                case CertificationSorting.FutureAll:
                    {
                        _examinations = await _examinationService.GetFutureExaminations(_selectedDateFrom);
                        _documentService.PrintFutureCertificationsAll(_examinations.ToList(), "Titla");
                        break;
                    }
                case CertificationSorting.FutureByUnit:
                    {
                        break;
                    }
                case CertificationSorting.FutureBySubgroup:
                    {
                        break;
                    }
                case CertificationSorting.FutureByExaminationType:
                    {
                        break;
                    }
                case CertificationSorting.ExpiredAll:
                    {
                        break;
                    }
                case CertificationSorting.ExpiredByUnit:
                    {
                        break;
                    }
                case CertificationSorting.ExpiredBySubgroup:
                    {
                        break;
                    }
                case CertificationSorting.ExpiredByExaminationType:
                    {
                        break;
                    }
            }
        }
    }

    internal enum CertificationSorting
    {
        FutureAll,
        FutureByUnit,
        FutureBySubgroup,
        FutureByExaminationType,
        ExpiredAll,
        ExpiredByUnit,
        ExpiredBySubgroup,
        ExpiredByExaminationType,
    }
}
