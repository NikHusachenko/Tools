using System.Collections.Generic;
using System.Threading;
using System.Windows;
using Tools.Common;
using Tools.Database.Entities;
using Tools.Services.ToolGroupServices;
using Tools.Services.ToolSubgroupServices;

namespace Tools.Desktop.Windows.CreateWindows
{
    public partial class CreateSubgroupWindow : Window
    {
        private readonly IToolSubgroupService _toolSubgroupService;
        private readonly IToolGroupService _toolGroupService;

        private readonly SemaphoreSlim _semaphore;

        public CreateSubgroupWindow(IToolSubgroupService toolSubgroupService,
            IToolGroupService toolGroupService)
        {
            _toolSubgroupService = toolSubgroupService;
            _toolGroupService = toolGroupService;

            _semaphore = new SemaphoreSlim(1);

            InitializeComponent();
        }

        private async void selectSubgroupComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            await _semaphore.WaitAsync(1);

            selectSubgroupComboBox.Items.Clear();
            ICollection<ToolSubgroupEntity> toolSubgroups = await _toolSubgroupService.GetAll();
            foreach (ToolSubgroupEntity toolSubgroup in toolSubgroups)
            {
                selectSubgroupComboBox.Items.Add($"{toolSubgroup.Name} | {toolSubgroup.Group.Name}");
            }

            if (toolSubgroupNewNameTextBox.IsLoaded)
            {
                toolSubgroupNewNameTextBox.Text = string.Empty;
            }

            _semaphore.Release();
        }

        private void selectSubgroupComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            subgroupRenameContainer.Visibility = Visibility.Visible;
            subgroupControlContainer.Visibility = Visibility.Visible;
            
            if (selectSubgroupComboBox.SelectedItem == null)
            {
                return;
            }

            string selectedSubgroupName = selectSubgroupComboBox.SelectedItem as string;
            if (selectedSubgroupName == null)
            {
                return;
            }

            toolSubgroupNewNameTextBox.Text = selectedSubgroupName.Split('|')[0].Trim(' ');
        }

        private async void selectGroupComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            await _semaphore.WaitAsync();

            selectGroupComboBox.Items.Clear();
            ICollection<ToolGroupEntity> toolGroups = await _toolGroupService.GetAll();
            foreach (ToolGroupEntity toolGroup in toolGroups)
            {
                selectGroupComboBox.Items.Add(toolGroup.Name);
            }

            _semaphore.Release();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string name = toolSubgroupNewNameTextBox.Text;
            if (string.IsNullOrEmpty(name))
            {
                return;
            }
            name = name.Split('|')[0].Trim(' ');

            var response = await _toolSubgroupService.GetByName(name);
            if (response.IsError)
            {
                return;
            }

            var deleteResult = await _toolSubgroupService.Delete(response.Value);
            if (deleteResult.IsError)
            {
                return;
            }

            MessageBox.Show(Messages.DELETED_SUCCESSFULY);
            toolSubgroupNewNameTextBox.Text = string.Empty;
            selectSubgroupComboBox_Loaded(sender, e);
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string oldName = selectSubgroupComboBox.SelectedItem as string;
            if (oldName == null)
            {
                toolSubgroupNewNameTextBox.Text = string.Empty;
                return;
            }
            string newName = toolSubgroupNewNameTextBox.Text.Split('|')[0].Trim(' ');

            var response = await _toolSubgroupService.Rename(oldName, newName);
            if (response.IsError)
            {
                toolSubgroupNewNameTextBox.Text = string.Empty;
                return;
            }

            selectSubgroupComboBox_Loaded(sender, e);
            MessageBox.Show(Messages.REMANED_SUCCESSFULY);
        }

        private async void createNewSubgroupButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedGroupName = selectGroupComboBox.SelectedItem as string;
            if (selectedGroupName == null)
            {
                subgroupNewNameTextBox.Text = string.Empty;
                return;
            }

            var response = await _toolSubgroupService.Create(subgroupNewNameTextBox.Text, selectedGroupName);
            if (response.IsError)
            {
                return;
            }

            MessageBox.Show(Messages.CREATED_SUCCESSFULY);
            subgroupNewNameTextBox.Text = string.Empty;
            selectSubgroupComboBox_Loaded(sender, e);
        }
    }
}