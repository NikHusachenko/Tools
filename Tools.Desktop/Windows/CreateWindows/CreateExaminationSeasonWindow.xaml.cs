using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Tools.Common;
using Tools.Database.Entities;
using Tools.Services.ExaminationReasonServices;

namespace Tools.Desktop.Windows.CreateWindows
{
    public partial class CreateExaminationSeasonWindow : Window
    {
        private readonly IExaminationReasonService _examinationReasonService;

        public CreateExaminationSeasonWindow(IExaminationReasonService examinationReasonService)
        {
            _examinationReasonService = examinationReasonService;

            InitializeComponent();
        }

        private void selectedReasonComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            reasonControlContainer.Visibility = Visibility.Visible;
            reasonRenameContainer.Visibility = Visibility.Collapsed;
            reasonRenameTextBox.Text = string.Empty;
        }

        private async void selectedReasonComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            selectedReasonComboBox.Items.Clear();

            ICollection<ExaminationReasonEntity> reasons = await _examinationReasonService.GetAll();
            foreach (ExaminationReasonEntity reason in reasons)
            {
                selectedReasonComboBox.Items.Add(reason.Name);
            }

            reasonRenameTextBox.Text = string.Empty;
        }

        private async void createNewReasonButton_Click(object sender, RoutedEventArgs e)
        {
            string name = newReasonNameTextBox.Text;
            if (string.IsNullOrEmpty(name))
            {
                newReasonNameTextBox.Text = string.Empty;
                return;
            }

            var response = await _examinationReasonService.Create(name);
            newReasonNameTextBox.Text = string.Empty;
            if (response.IsError)
            {
                return;
            }

            MessageBox.Show(Messages.CREATED_SUCCESSFULY);
            selectedReasonComboBox_Loaded(sender, e);
        }

        private async void deleteReasonButton_Click(object sender, RoutedEventArgs e)
        {
            string name = selectedReasonComboBox.SelectedItem as string;
            if (name == null)
            {
                reasonRenameTextBox.Text = string.Empty;
                return;
            }

            var response = await _examinationReasonService.Delete(name);
            reasonRenameTextBox.Text = string.Empty;
            if (response.IsError)
            {
                return;
            }

            MessageBox.Show(Messages.DELETED_SUCCESSFULY);
            selectedReasonComboBox_Loaded(sender, e);
        }

        private async void renameReasonButton_Click(object sender, RoutedEventArgs e)
        {
            string oldName = selectedReasonComboBox.SelectedItem as string;
            if (oldName == null)
            {
                return;
            }
            string newName = reasonRenameTextBox.Text;

            var response = await _examinationReasonService.Rename(oldName, newName);
            if (response.IsError)
            {
                reasonRenameTextBox.Text = string.Empty;
                return;
            }

            MessageBox.Show(Messages.REMANED_SUCCESSFULY);
            selectedReasonComboBox_Loaded(sender, e);
        }
    }
}