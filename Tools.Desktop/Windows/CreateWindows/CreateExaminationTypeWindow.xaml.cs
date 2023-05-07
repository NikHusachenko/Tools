using System.Collections.Generic;
using System.Windows;
using Tools.Common;
using Tools.Database.Entities;
using Tools.Services.ExaminationTypeService;

namespace Tools.Desktop.Windows.CreateWindows
{
    public partial class CreateExaminationTypeWindow : Window
    {
        private readonly IExaminationTypeService _examinationTypeService;

        public CreateExaminationTypeWindow(IExaminationTypeService examinationTypeService)
        {
            _examinationTypeService = examinationTypeService;

            InitializeComponent();
        }

        private async void createNewTypeButton_Click(object sender, RoutedEventArgs e)
        {
            string name = newTypeNameTextBox.Text;

            var response = await _examinationTypeService.Create(name);
            newTypeNameTextBox.Text = string.Empty;
            if (response.IsError)
            {
                return;
            }

            MessageBox.Show(Messages.CREATED_SUCCESSFULY);
            selectTypeComboBox_Loaded(sender, e);
        }

        private void selectTypeComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            string selectedName = selectTypeComboBox.SelectedItem as string;
            if (selectedName == null)
            {
                renameTypeTextBox.Text = string.Empty;
                return;
            }

            typeControlContainer.Visibility = Visibility.Visible;
            typeRenameContainer.Visibility = Visibility.Visible;
            renameTypeTextBox.Text = selectedName;
        }

        private async void selectTypeComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            selectTypeComboBox.Items.Clear();

            ICollection<ExaminationTypeEntity> types = await _examinationTypeService.GetAll();
            foreach (ExaminationTypeEntity type in types)
            {
                selectTypeComboBox.Items.Add(type.Name);
            }

            renameTypeTextBox.Text = string.Empty;
        }

        private async void deleteTypeButotn_Click(object sender, RoutedEventArgs e)
        {
            string name = selectTypeComboBox.SelectedItem as string;
            if (name == null)
            {
                renameTypeTextBox.Text = string.Empty;
                return;
            }

            var response = await _examinationTypeService.Delete(name);
            if (response.IsError)
            {
                renameTypeTextBox.Text = string.Empty;
                return;
            }

            MessageBox.Show(Messages.DELETED_SUCCESSFULY);
            selectTypeComboBox_Loaded(sender, e);
        }

        private async void renameTypeButton_Click(object sender, RoutedEventArgs e)
        {
            string oldName = selectTypeComboBox.SelectedItem as string;
            if (oldName == null)
            {
                renameTypeTextBox.Text = string.Empty;
                return;
            }

            string newName = renameTypeTextBox.Text;
            if (string.IsNullOrEmpty(newName))
            {
                renameTypeTextBox.Text = string.Empty;
                return;
            }

            var response = await _examinationTypeService.Rename(oldName, newName);
            if (response.IsError)
            {
                renameTypeTextBox.Text = string.Empty;
                return;
            }

            MessageBox.Show(Messages.REMANED_SUCCESSFULY);
            selectTypeComboBox_Loaded(sender, e);
        }
    }
}