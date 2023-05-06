using System.Collections.Generic;
using System.Windows;
using Tools.Common;
using Tools.Database.Entities;
using Tools.Services.ToolGroupServices;

namespace Tools.Desktop.Windows.CreateWindows
{
    public partial class CreateGroupWindow : Window
    {
        private readonly IToolGroupService _toolGroupService;

        public CreateGroupWindow(IToolGroupService toolGroupService)
        {
            _toolGroupService = toolGroupService;

            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            selectedGroupControlContainer.Visibility = Visibility.Visible;
            selectedGroupRenameContainer.Visibility = Visibility.Visible;
            
            string groupName = selectGroupComboBox.SelectedItem as string;
            if (groupName == null)
            {
                return;
            }
            selectedGroupNameTextBox.Text = groupName;
        }

        private async void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            ICollection<ToolGroupEntity> toolGroups = await _toolGroupService.GetAll();
            foreach (ToolGroupEntity toolGroupEntity in toolGroups)
            {
                selectGroupComboBox.Items.Add(toolGroupEntity.Name);
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string groupName = selectGroupComboBox.SelectedItem as string;
            if (groupName == null)
            {
                return;
            }

            var response = await _toolGroupService.Delete(groupName);
            if (response.IsError)
            {
                return;
            }

            selectGroupComboBox.Items.Remove(groupName);
            selectedGroupNameTextBox.Text = string.Empty;
        }

        private async void renameGroupButton_Click(object sender, RoutedEventArgs e)
        {
            string groupName = selectGroupComboBox.SelectedItem as string;
            if (groupName == null)
            {
                return;
            }

            var response = await _toolGroupService.GetByName(groupName);
            string newName = selectedGroupNameTextBox.Text;
            response.Value.Name = newName;

            var updateResult = await _toolGroupService.Update(response.Value);
            if (updateResult.IsError)
            {
                return;
            }

            selectGroupComboBox.Items.Remove(groupName);
            selectGroupComboBox.Items.Add(newName);
            selectedGroupNameTextBox.Text = string.Empty;
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string newName = createNewGroupName.Text;
            var response = await _toolGroupService.Create(newName);
            if (response.IsError)
            {
                return;
            }

            createNewGroupName.Text = string.Empty;
            MessageBox.Show(Messages.CREATED_SUCCESSFULY);
        }
    }
}